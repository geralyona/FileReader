using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Castle.Core.Internal;
using CommunityToolkit.Mvvm.Input;
using FileReader.Common.Data;
using FileReader.Common.Exceptions;
using FileReader.Interfaces.Logic;
using FileReader.Interfaces.Progress;
using FileReader.Interfaces.UI;
using FileReader.Interfaces.Words;
using FileReader.ViewModels.Base;
using FileReader.ViewModels.Operation;
using Ninject;

namespace FileReader.ViewModels
{
    public class MainVM : ViewModelBase
    {
        private const double WordsCalculationWeight = 0.9;
        private const int ProgressTimerRefreshDelay = 100;

        private double _progress;
        private string _statusMessage = string.Empty;
        private IReadOnlyList<WordCount> _counts = Array.Empty<WordCount>();

        [Inject] public ICountsCalculator CountsCalculator { private get; set; } = null!;

        [Inject] public ICharsProviderFactory CharsProviderFactory { private get; set; } = null!;

        [Inject] public IUIDispatcher UIDispatcher { private get; set; } = null!;

        [Inject] public IMessager Messager { private get; set; } = null!;

        [Inject] public IProgressFactory ProgressFactory { private get; set; } = null!;

        [Inject] public FileSlectorVM SelectFileNameVM { get; set; } = null!;

        [Inject] public ProgressStatusVM ProgressStoreVM { get; set; } = null!;

        public MainVM()
        {
            StartProcessingCommand = new RelayCommand(StartProcessing, () => Cts == null && !SelectFileNameVM.FileName.IsNullOrEmpty());
            StopProcessingCommand = new RelayCommand(() => Cts?.Cancel(), () => Cts != null);
        }

        [Inject]

        public void Init()
        {
            SelectFileNameVM.PropertyChanged += SelectFileNameVM_PropertyChanged;
        }

        private void SelectFileNameVM_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StartProcessingCommand.NotifyCanExecuteChanged();
        }

        CancellationTokenSource? _cts = null!;
        private CancellationTokenSource? Cts
        {
            get => _cts;
            set
            {
                _cts = value;
                StartProcessingCommand.NotifyCanExecuteChanged();
                StopProcessingCommand.NotifyCanExecuteChanged();
            }
        }

        public double Progress
        {
            get => ProgressStoreVM.Progress;
            set => ProgressStoreVM.Progress = value;
        }

        public string StatusMessage
        {
            get => ProgressStoreVM.StatusMessage;
            set => ProgressStoreVM.StatusMessage = value;
        }

        public IReadOnlyList<WordCount> Counts
        {
            get => _counts;
            set => SetField(ref _counts, value);
        }

        public RelayCommand StopProcessingCommand { get; private set; }

        public RelayCommand StartProcessingCommand { get; private set; }

        private async void StartProcessing()
        {
            if (!File.Exists(SelectFileNameVM.FileName))
            {
                Messager.ShowMessage("Invalid file name");
                return;
            }

            Progress = 0;
            Counts = new List<WordCount>();
            StatusMessage = "Processing started";
            Cts = new CancellationTokenSource();
            var progress = CreateProgress();
            var charsProvider = CharsProviderFactory.CreateFileAsciiCharsProvider(SelectFileNameVM.FileName);

            var timer = new System.Timers.Timer(ProgressTimerRefreshDelay);
            timer.Elapsed += (o, a) =>
            {
                UIDispatcher.ScheduleOnUI(() =>
                {
                    ProgressStoreVM.RaiseAllPropertyChanged();
                });
            };

            try
            {
                timer.Start();
                var data = await Task.Run(() => CountsCalculator.CalculateWordCounts(charsProvider, progress, Cts.Token));
                timer.Stop();
                var canceled = Cts.IsCancellationRequested;
                SetProgress(Progress, "Sorting...");
                ProgressStoreVM.RaiseAllPropertyChanged();

                var counts = await Task.Run(() =>
                {
                    return data.OrderByDescending(a => a.Count).ThenBy(a => a.Word).ToList();
                });

                var canceledString = Cts.IsCancellationRequested ? "Canceled: " : string.Empty;

                SetProgress(canceled ? Progress : 1, $"{canceledString}{counts.Count} words resolved");
                ProgressStoreVM.RaiseAllPropertyChanged();

                Counts = counts;
                Cts = null;
            }
            catch (CharsProviderException e)
            {
                StatusMessage = e.Message;
                Cts = null;
            }
        }

        private IProgress<ProcessingState> CreateProgress() => ProgressFactory.CreateProgress(StoreProgress);

        private void StoreProgress(ProcessingState state)
        {
            var value = state.Progres * WordsCalculationWeight; ;
            if (value < ProgressStoreVM.Progress)
            {
                // unexpected progress reporting order
                return;
            }

            ProgressStoreVM.SetProgressNotNotify(state.Progres, state.Message);
        }

        private void SetProgress(double progress, string? message)
        {
            Progress = progress;
            if (message != null)
                StatusMessage = message;
        }
    }
}