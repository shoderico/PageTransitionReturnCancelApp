namespace PageTransitionReturnCancelApp;

public partial class TransitionSourcePage1 : ContentPage
{
    public TransitionSourcePage1()
    {
        InitializeComponent();
    }

    // Handles the button click to navigate to TransitionDestinationPage1
    private async void OnTransitClicked(object sender, EventArgs e)
    {
        // Create a TaskCompletionSource to receive the return value
        var tcs = new TaskCompletionSource<string>();

        // Navigate to TransitionDestinationPage1, passing the TaskCompletionSource
        await Shell.Current.Navigation.PushAsync(new TransitionDestinationPage1(tcs));

        // Wait for the result from TransitionDestinationPage1
        var result = await tcs.Task;

        // Display the result in an alert
        await DisplayAlert("Return value", $"result from TransitionDestinationPage1: {result}", "OK");
    }
}