namespace PageTransitionReturnCancelApp;

public partial class TransitionDestinationPage2 : ContentPage
{
    private TaskCompletionSource<string> _tcs;
    private CancellationReasonTokenSource _cts;

    // Constructor: Receives TaskCompletionSource and CancellationReasonTokenSource
    public TransitionDestinationPage2(TaskCompletionSource<string> tcs, CancellationReasonTokenSource cts)
    {
        InitializeComponent();
        _tcs = tcs;
        _cts = cts;
    }

    // Handles the "Set Result" button click
    private async void OnSetResultClicked(object sender, EventArgs e)
    {
        Console.WriteLine("OnSetResultClicked");

        // Set a successful result
        _tcs.TrySetResult("'This value was set from TransitionDestinationPage2'");
        await Shell.Current.Navigation.PopAsync();
    }

    // Handles the "Cancel" button click
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        Console.WriteLine("OnCancelClicked");

        // Cancel with specific reason
        _cts.Cancel(CancelReason.OnCancelButtonClicked);
        await Shell.Current.Navigation.PopAsync();
    }

    // Handles Android hardware back button
    protected override bool OnBackButtonPressed()
    {
        Console.WriteLine("OnBackButtonPressed");

        // Cancel with specific reason for hardware back button
        _cts.Cancel(CancelReason.OnBackButtonPressed);
        Shell.Current.Navigation.PopAsync();
        return true; // Prevent default back behavior
    }

    // Handles page disappearance (e.g., top-left back button or iOS swipe)
    protected override void OnDisappearing()
    {
        Console.WriteLine("OnDisappearing");

        base.OnDisappearing();
        if (!_tcs.Task.IsCompleted)
        {
            // Cancel with specific reason if Task is not completed
            _cts.Cancel(CancelReason.OnDisappearing);
        }
    }
}