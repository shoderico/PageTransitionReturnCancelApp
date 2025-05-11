# PageTransitionReturnCancelApp

A .NET MAUI demo application showcasing page navigation with return values and cancellation handling using `TaskCompletionSource` and a custom `CancellationTokenSource`. The app features a drawer menu (Shell Flyout) and demonstrates different navigation scenarios, including returning results and handling cancellations with specific reasons.

## Features
- **Drawer Menu Navigation**: Uses .NET MAUI Shell with a flyout menu to navigate between pages (`MainPage`, `TransitionSourcePage1`, `TransitionSourcePage2`).
- **Return Value Handling**: Uses `TaskCompletionSource` to return values from destination pages (`TransitionDestinationPage1`, `TransitionDestinationPage2`) to source pages.
- **Cancellation with Reasons**: Implements a custom `CancellationReasonTokenSource` to handle cancellations with specific reasons (e.g., Cancel button, back button, page disappearance).
- **Platform-Specific Back Navigation**: Handles Android hardware back button, iOS swipe back, and top-left corner back button with distinct return values or cancellation reasons.

## Prerequisites
- **.NET 8.0 SDK** or later
- **Visual Studio 2022** (or VS Code with MAUI workload)
- **.NET MAUI workload** installed (`dotnet workload install maui`)
- Android, iOS, or Windows emulator/simulator for testing

## Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/PageTransitionReturnCancelApp.git
   ```
2. Open the solution (`PageTransitionReturnCancelApp.sln`) in Visual Studio.
3. Restore NuGet packages:
   ```bash
   dotnet restore
   ```
4. Set the target platform (Android, iOS, or Windows) and run the app.

## Usage
1. Launch the app to see the `MainPage`.
2. Open the drawer menu (hamburger icon or swipe from left) to navigate to:
   - **TransitionSourcePage1**: Demonstrates basic navigation to `TransitionDestinationPage1` with return values.
   - **TransitionSourcePage2**: Demonstrates navigation to `TransitionDestinationPage2` with cancellation handling.
3. **TransitionSourcePage1**:
   - Click "Transit to TransitionDestinationPage1" to navigate.
   - On `TransitionDestinationPage1`, use the "Back" button, Android hardware back, top-left back button, or iOS swipe to return with different results.
   - Result is displayed in an alert (e.g., "Back by Back Button on Page").
4. **TransitionSourcePage2**:
   - Click "Transit to TransitionDestinationPage2" to navigate.
   - On `TransitionDestinationPage2`, use:
     - "Set Result" to return a value.
     - "Cancel" to cancel with reason `OnCancelButtonClicked`.
     - Android hardware back for reason `OnBackButtonPressed`.
     - Top-left back button or iOS swipe for reason `OnDisappearing`.
   - Result or cancellation reason is displayed in an alert.

## File Structure
- **AppShell.xaml**: Defines the Shell structure with a flyout menu for navigation.
- **TransitionSourcePage1.xaml/.cs**: Source page for navigating to `TransitionDestinationPage1` and receiving return values.
- **TransitionSourcePage2.xaml/.cs**: Source page for navigating to `TransitionDestinationPage2`, handling cancellations with `CancellationReasonTokenSource`.
- **TransitionDestinationPage1.xaml/.cs**: Destination page returning different results based on back navigation method.
- **TransitionDestinationPage2.xaml/.cs**: Destination page supporting result return and cancellation with specific reasons.
- **CancellationReasonTokenSource.cs** (in `TransitionSourcePage2.xaml.cs`): Custom class extending `CancellationTokenSource` to manage cancellation reasons.

## Notes
- The app uses `TaskCompletionSource` for asynchronous return value handling.
- `CancellationReasonTokenSource` and `CancelReason` enum enable detailed cancellation tracking.
- Debug logs (`Console.WriteLine`) are included for navigation and cancellation events.

## License
MIT License

---
Copyright (c) 2025 shoderico