namespace AnketaApp1;

public partial class MainPage : ContentPage
{
    // Начальные значения — те же, что заданы в XAML (нужны для кнопки «Очистить»)
    private static readonly DateTime DefaultTripDate = new(2027, 6, 1);
    private const double DefaultTravelers = 2;
    private const double DefaultBudget = 100000;

    public MainPage()
    {
        InitializeComponent();

        // Анкета оформлена в светлых тонах — фиксируем светлую тему,
        // чтобы в тёмной теме Windows/Android текст не становился белым
        if (Application.Current is not null)
            Application.Current.UserAppTheme = AppTheme.Light;

        UpdateResultButtonState();
    }

    // Срабатывает при вводе имени и телефона
    private void OnRequiredFieldChanged(object sender, TextChangedEventArgs e)
    {
        UpdateResultButtonState();
    }

    // IsEnabled / IsVisible меняются в зависимости от заполнения анкеты
    private void UpdateResultButtonState()
    {
        bool filled = !string.IsNullOrWhiteSpace(EntryName.Text)
                      && !string.IsNullOrWhiteSpace(EntryPhone.Text);

        ButtonShowResult.IsEnabled = filled;
        LabelHint.IsVisible = !filled;
    }

    private async void OnShowResultClicked(object sender, EventArgs e)
    {
        // Проверка обязательных полей
        var errors = new List<string>();

        string name = EntryName.Text?.Trim() ?? string.Empty;
        string phone = EntryPhone.Text?.Trim() ?? string.Empty;

        if (name.Length < 2)
            errors.Add("• Имя должно содержать не меньше 2 символов.");

        if (phone.Count(char.IsDigit) < 10)
            errors.Add("• В телефоне должно быть не меньше 10 цифр.");

        if (PickerDestination.SelectedIndex < 0)
            errors.Add("• Выберите направление.");

        if (errors.Count > 0)
        {
            LabelErrors.Text = string.Join(Environment.NewLine, errors);
            LabelErrors.IsVisible = true;
            ResultPanel.IsVisible = false;
            await ScrollMain.ScrollToAsync(LabelErrors, ScrollToPosition.Center, true);
            return;
        }

        LabelErrors.IsVisible = false;

        // Сбор ответов
        string tripType = new[] { RadioBeach, RadioExcursion, RadioActive }
            .FirstOrDefault(r => r.IsChecked)?.Content?.ToString() ?? "не выбран";

        var services = new List<string>();
        if (CheckTransfer.IsChecked) services.Add("трансфер");
        if (CheckInsurance.IsChecked) services.Add("страховка");
        string servicesText = services.Count > 0 ? string.Join(", ", services) : "не нужны";

        string wishes = string.IsNullOrWhiteSpace(EditorWishes.Text) ? "нет" : EditorWishes.Text.Trim();

        LabelResult.Text =
            $"Имя: {name}\n" +
            $"Телефон: {phone}\n" +
            $"Направление: {PickerDestination.SelectedItem}\n" +
            $"Тип отдыха: {tripType}\n" +
            $"Дата вылета: {DatePickerTrip.Date:dd.MM.yyyy}\n" +
            $"Путешественников: {StepperTravelers.Value:0}\n" +
            $"Бюджет на человека: {Math.Round(SliderBudget.Value / 1000) * 1000:N0} ₽\n" +
            $"Доп. услуги: {servicesText}\n" +
            $"Рассылка: {(SwitchNewsletter.IsToggled ? "да" : "нет")}\n" +
            $"Пожелания: {wishes}";

        ResultPanel.IsVisible = true;
        await ScrollMain.ScrollToAsync(ResultPanel, ScrollToPosition.End, true);
    }

    // Возвращает все поля и результат в исходное состояние
    private async void OnClearClicked(object sender, EventArgs e)
    {
        EntryName.Text = string.Empty;
        EntryPhone.Text = string.Empty;
        EditorWishes.Text = string.Empty;

        PickerDestination.SelectedIndex = -1;

        RadioBeach.IsChecked = false;
        RadioExcursion.IsChecked = false;
        RadioActive.IsChecked = false;

        DatePickerTrip.Date = DefaultTripDate;
        StepperTravelers.Value = DefaultTravelers;
        SliderBudget.Value = DefaultBudget;

        CheckTransfer.IsChecked = false;
        CheckInsurance.IsChecked = true;
        SwitchNewsletter.IsToggled = true;

        LabelErrors.Text = string.Empty;
        LabelErrors.IsVisible = false;
        LabelResult.Text = string.Empty;
        ResultPanel.IsVisible = false;

        UpdateResultButtonState();
        await ScrollMain.ScrollToAsync(0, 0, true);
    }
}
