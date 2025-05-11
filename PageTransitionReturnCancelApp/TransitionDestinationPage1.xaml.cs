namespace PageTransitionReturnCancelApp;

public partial class TransitionDestinationPage1 : ContentPage
{
    private readonly TaskCompletionSource<string> _tcs;

    // Constructor: Receives TaskCompletionSource for returning result
    public TransitionDestinationPage1(TaskCompletionSource<string> tcs)
    {
        InitializeComponent();
        _tcs = tcs;
    }

    // Handles the "Back" button click
    private async void OnBackClicked(object sender, EventArgs e)
    {
        Console.WriteLine("OnBackClicked");

        // Set result for custom back button
        _tcs.TrySetResult("Back by Back Button on Page");
        await Shell.Current.Navigation.PopAsync();
    }

    // Handles Android hardware back button
    protected override bool OnBackButtonPressed()
    {
        Console.WriteLine("OnBackButtonPressed");

        // Set result for hardware back button
        _tcs.TrySetResult("Back by Android:[Hardware Back Button] / Windows:[Top-Left Corner Back Button]");
        Shell.Current.Navigation.PopAsync();

        return true; // Prevent default back behavior
    }

    // Handles page disappearance (e.g., top-left back button or iOS swipe)
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Console.WriteLine("OnDisappearing");

        // Set default result if Task is not completed
        if (!_tcs.Task.IsCompleted)
        {
            _tcs.TrySetResult("Back by Android:[Top-Left Corner Back Button] / iOS:[Swipe Back]");
        }
    }
}