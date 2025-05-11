namespace PageTransitionReturnCancelApp;

// Enum to define cancellation reasons
public enum CancelReason
{
    Unknown = 0,
    OnCancelButtonClicked,
    OnBackButtonPressed,
    OnDisappearing
}

// Custom CancellationTokenSource to store cancellation reason
public class CancellationReasonTokenSource : CancellationTokenSource
{
    public CancelReason Reason { get; private set; } = CancelReason.Unknown;

    // Sets the cancellation reason and triggers cancellation
    public void Cancel(CancelReason reason)
    {
        Reason = reason;
        base.Cancel();
    }
}

public partial class TransitionSourcePage2 : ContentPage
{
    public TransitionSourcePage2()
    {
        InitializeComponent();
    }

    // Handles the button click to navigate to TransitionDestinationPage2
    private async void OnTransitClicked(object sender, EventArgs e)
    {
        // Create TaskCompletionSource for return value
        var tcs = new TaskCompletionSource<string>();

        // Create CancellationReasonTokenSource for cancellation handling
        var cts = new CancellationReasonTokenSource();

        // Register callback to set cancellation exception with reason
        cts.Token.Register(() => tcs.TrySetException(new OperationCanceledException(cts.Reason.ToString())));

        // Navigate to TransitionDestinationPage2, passing tcs and cts
        await Shell.Current.Navigation.PushAsync(new TransitionDestinationPage2(tcs, cts));

        try
        {
            // Wait for the result or cancellation
            var result = await tcs.Task;
            await DisplayAlert("Return value", $"result from TransitionDestinationPage2: {result}", "OK");
        }
        catch (OperationCanceledException ex)
        {
            // Handle cancellation and display the reason
            await DisplayAlert("Cancel", $"Cancelled.\r\nCancelReason: {ex.Message}", "OK");
        }
        finally
        {
            // Clean up CancellationReasonTokenSource
            cts.Dispose();
            cts = null;
        }
    }
}