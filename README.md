# Анкета путешественника (.NET MAUI)

Домашнее задание №1 «Анкета» по курсу «Современные технологии разработки мобильных приложений».

MAUI-приложение с анкетой для подбора тура: имя, телефон, направление, тип отдыха, дата вылета, количество путешественников, бюджет, дополнительные услуги, рассылка и пожелания.

## Что реализовано

- Контролы: Label, Entry (Text/Telephone), Editor, Picker, RadioButton (группа TripType), CheckBox, Switch, DatePicker, Stepper, Slider, Image, Button.
- Разметка: Grid с RowDefinitions/ColumnDefinitions, VerticalStackLayout и HorizontalStackLayout со Spacing, Margin/Padding, ScrollView.
- Привязки через `x:Reference` (Path, Mode) для Stepper и Slider.
- «Показать результат» активна только после ввода имени и телефона, проверяет обязательные поля и выводит сводку.
- «Очистить» возвращает анкету в исходное состояние.

Основные файлы: `AnketaApp1/MainPage.xaml`, `AnketaApp1/MainPage.xaml.cs`.

## Запуск

Открыть `AnketaApp1.slnx` в Visual Studio с нагрузкой .NET MAUI и запустить на Windows Machine или Android-эмуляторе.
