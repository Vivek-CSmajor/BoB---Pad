using System;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using ReactiveUI;
namespace BoB___Pad.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _textContent;

    public string TextContent
    {
        get => _textContent;
        set => this.RaiseAndSetIfChanged(ref _textContent, value);
    }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> OpenCommand { get; }

    public MainWindowViewModel()
    {
        var uiScheduler = RxApp.MainThreadScheduler;

        SaveCommand = ReactiveCommand.CreateFromTask(SaveToFileAsync);
        OpenCommand = ReactiveCommand.CreateFromTask(OpenFileAsync);

    }
    private async Task SaveToFileAsync()
    {
        var content = TextContent ?? string.Empty;
        await File.WriteAllTextAsync("output.txt", content);
    }


    private async Task OpenFileAsync()
    {
        var dialog = new OpenFileDialog
        {
            Title = "Open File",
            AllowMultiple = false
        };

        var window = (App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
        if (window == null) return;

        var result = await Dispatcher.UIThread.InvokeAsync(() => dialog.ShowAsync(window));
        if (result != null && result.Length == 1)
        {
            var context = await File.ReadAllTextAsync(result[0]);
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                TextContent = context;
            });
        }
    }
}