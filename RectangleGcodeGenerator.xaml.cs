using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace GcodeGenerator
{
    public partial class RectangleGcodeGenerator : Window
    {
        public RectangleGcodeGenerator()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            double x, y;

            if (!double.TryParse(XTextBox.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out x) ||
                !double.TryParse(YTextBox.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out y))
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения для длины и ширины детали.");
                return;
            }

            if (x <= 0 || y <= 0)
            {
                MessageBox.Show("Координаты 'X' и 'Y' должны быть больше 0.");
                return;
            }

            // Проверяем, чтобы длина и ширина не превышали максимальные значения
            if (x > 3000 || y > 1200)
            {
                MessageBox.Show("Координата 'X' не может превышать 3000, а координата 'Y' 1200.");
                return;
            }

            // Проверяем, чтобы координаты X и Y не были отрицательными
            if (x <= 0 || y <= 0)
            {
                MessageBox.Show("Координаты 'X' и 'Y' не могут быть отрицательными.");
                return;
            }

            int selectedNumber = NumberComboBox.SelectedIndex;

            StringBuilder gcodeBuilder = new StringBuilder();

            // В зависимости от выбранного числа генерируем соответствующий код
            switch (selectedNumber)
            {
                default:
                    gcodeBuilder.AppendLine($"; X = {x}, Y = {y}, кол-во перемычек = 0");
                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Начало программы");
                    gcodeBuilder.AppendLine("%");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к абсолютным координатам и перемещение на начало координат");
                    gcodeBuilder.AppendLine("N01 G90");
                    gcodeBuilder.AppendLine("N02 G54");
                    gcodeBuilder.AppendLine("N03 M13()");
                    gcodeBuilder.AppendLine("N04 G0 G42 X-3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к относительным координатам и построение сторон детали");
                    gcodeBuilder.AppendLine("N05 G91");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD");
                    gcodeBuilder.AppendLine("N06 M10()");
                    gcodeBuilder.AppendLine($"N07 G1 G42 X{Math.Round((x + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N08 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами AD и DC");
                    gcodeBuilder.AppendLine($"N09 G0 G42 X0.25 Y-3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC");
                    gcodeBuilder.AppendLine("N10 M10()");
                    gcodeBuilder.AppendLine($"N11 G1 G42 X0 Y{Math.Round((y + 3), 2)}");
                    gcodeBuilder.AppendLine("N12 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами DC и CB");
                    gcodeBuilder.AppendLine($"N13 G0 G42 X3 Y0.25");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB");
                    gcodeBuilder.AppendLine("N14 M10()");
                    gcodeBuilder.AppendLine($"N15 G1 G42 X-{Math.Round((x + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N16 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами CB и BA");
                    gcodeBuilder.AppendLine($"N17 G0 G42 X-0.25 Y3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA");
                    gcodeBuilder.AppendLine("N18 M10()");
                    gcodeBuilder.AppendLine($"N19 G1 G42 X0 Y-{Math.Round((y + 3), 2)}");
                    gcodeBuilder.AppendLine("N20 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Конец программы");
                    gcodeBuilder.AppendLine("N21 M30");

                    GCodeTextBox.Text = gcodeBuilder.ToString().Replace(",", ".");
                    break;

                case 0:

                    gcodeBuilder.AppendLine($"; X = {x}, Y = {y}, кол-во перемычек = 0");
                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Начало программы");
                    gcodeBuilder.AppendLine("%");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к абсолютным координатам и перемещение на начало координат");
                    gcodeBuilder.AppendLine("N01 G90");
                    gcodeBuilder.AppendLine("N02 G54");
                    gcodeBuilder.AppendLine("N03 M13()");
                    gcodeBuilder.AppendLine("N04 G0 G42 X-3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к относительным координатам и построение сторон детали");
                    gcodeBuilder.AppendLine("N05 G91");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD");
                    gcodeBuilder.AppendLine("N06 M10()");
                    gcodeBuilder.AppendLine($"N07 G1 G42 X{Math.Round((x + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N08 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами AD и DC");
                    gcodeBuilder.AppendLine($"N09 G0 G42 X0.25 Y-3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC");
                    gcodeBuilder.AppendLine("N10 M10()");
                    gcodeBuilder.AppendLine($"N11 G1 G42 X0 Y{Math.Round((y + 3), 2)}");
                    gcodeBuilder.AppendLine("N12 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами DC и CB");
                    gcodeBuilder.AppendLine($"N13 G0 G42 X3 Y0.25");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB");
                    gcodeBuilder.AppendLine("N14 M10()");
                    gcodeBuilder.AppendLine($"N15 G1 G42 X-{Math.Round((x + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N16 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами CB и BA");
                    gcodeBuilder.AppendLine($"N17 G0 G42 X-0.25 Y3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA");
                    gcodeBuilder.AppendLine("N18 M10()");
                    gcodeBuilder.AppendLine($"N19 G1 G42 X0 Y-{Math.Round((y + 3), 2)}");
                    gcodeBuilder.AppendLine("N20 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Конец программы");
                    gcodeBuilder.AppendLine("N21 M30");

                    GCodeTextBox.Text = gcodeBuilder.ToString().Replace(",", ".");
                    break;

                case 1:

                    gcodeBuilder.AppendLine($"; X = {x}, Y = {y}, кол-во перемычек на каждой стороне = 1, длина перемычек = 0.3");
                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Начало программы");
                    gcodeBuilder.AppendLine("%");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к абсолютным координатам и перемещение на начало координат");
                    gcodeBuilder.AppendLine("N01 G90");
                    gcodeBuilder.AppendLine("N02 G54");
                    gcodeBuilder.AppendLine("N03 M13()");
                    gcodeBuilder.AppendLine($"N04 G0 G42 X-{Math.Round((3 - 0.55 * selectedNumber), 2)} Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к относительным координатам и построение сторон детали");
                    gcodeBuilder.AppendLine("N05 G91");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (1/2)");
                    gcodeBuilder.AppendLine("N06 M10()");
                    gcodeBuilder.AppendLine($"N07 G1 G42 X{Math.Round((x / 2 + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N08 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка на стороне AD");
                    gcodeBuilder.AppendLine("N09 G0 G42 X0.3 Y0");


                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (2/2)");
                    gcodeBuilder.AppendLine("N10 M10()");
                    gcodeBuilder.AppendLine($"N11 G1 G42 X{Math.Round(((x / 2) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N12 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами AD и DC");
                    gcodeBuilder.AppendLine("N13 G0 G42 X0.25 Y-3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (1/2)");
                    gcodeBuilder.AppendLine("N14 M10()");
                    gcodeBuilder.AppendLine($"N15 G1 G42 X0 Y{Math.Round(((y / 2) + 3), 2)}");
                    gcodeBuilder.AppendLine("N16 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка на стороне DC");
                    gcodeBuilder.AppendLine("N17 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (2/2)");
                    gcodeBuilder.AppendLine("N18 M10()");
                    gcodeBuilder.AppendLine($"N19 G1 G42 X0 Y{Math.Round(((y / 2) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N20 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами DC и CB");
                    gcodeBuilder.AppendLine("N21 G0 G42 X3 Y0.25");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (1/2)");
                    gcodeBuilder.AppendLine("N22 M10()");
                    gcodeBuilder.AppendLine($"N23 G1 G42 X-{Math.Round(((x / 2) + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N24 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка на стороне CB");
                    gcodeBuilder.AppendLine("N25 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (2/2)");
                    gcodeBuilder.AppendLine("N26 M10()");
                    gcodeBuilder.AppendLine($"N27 G1 G42 X-{Math.Round(((x / 2) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N28 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами CB и BA");
                    gcodeBuilder.AppendLine("N29 G0 G42 X-0.25 Y3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (1/2)");
                    gcodeBuilder.AppendLine("N30 M10()");
                    gcodeBuilder.AppendLine($"N31 G1 G42 X0 Y-{Math.Round(((y / 2) + 3), 2)}");
                    gcodeBuilder.AppendLine("N32 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка на стороне BA");
                    gcodeBuilder.AppendLine("N33 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (2/2)");
                    gcodeBuilder.AppendLine("N34 M10()");
                    gcodeBuilder.AppendLine($"N35 G1 G42 X0 Y-{Math.Round(((y / 2) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N36 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Конец программы");
                    gcodeBuilder.AppendLine("N37 M30");

                    GCodeTextBox.Text = gcodeBuilder.ToString().Replace(",", ".");
                    break;

                case 2:

                    gcodeBuilder.AppendLine($"; X = {x}, Y = {y}, кол-во перемычек на каждой стороне = 2, длина перемычек = 0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Начало программы");
                    gcodeBuilder.AppendLine("%");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к абсолютным координатам и перемещение на начало координат");
                    gcodeBuilder.AppendLine("N01 G90");
                    gcodeBuilder.AppendLine("N02 G54");
                    gcodeBuilder.AppendLine("N03 M13()");
                    gcodeBuilder.AppendLine("N04 G0 G42 X-3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к относительным координатам и построение сторон детали");
                    gcodeBuilder.AppendLine("N05 G91");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (1/3)");
                    gcodeBuilder.AppendLine("N06 M10()");
                    gcodeBuilder.AppendLine($"N07 G1 G42 X{Math.Round((x / 3 + 3), 2)} Y0"); 
                    gcodeBuilder.AppendLine("N08 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне AD");
                    gcodeBuilder.AppendLine("N09 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (2/3)");
                    gcodeBuilder.AppendLine("N10 M10()");
                    gcodeBuilder.AppendLine($"N11 G1 G42 X{Math.Round((x / 3), 2)} Y0"); 
                    gcodeBuilder.AppendLine("N12 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне AD");
                    gcodeBuilder.AppendLine("N13 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (3/3)");
                    gcodeBuilder.AppendLine("N14 M10()");
                    gcodeBuilder.AppendLine($"N15 G1 G42 X{Math.Round(((x / 3) - 0.55 * selectedNumber),2)} Y0");
                    gcodeBuilder.AppendLine("N16 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами AD и DC");
                    gcodeBuilder.AppendLine("N17 G0 G42 X0.25 Y-3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (1/3)");
                    gcodeBuilder.AppendLine("N18 M10()");
                    gcodeBuilder.AppendLine($"N19 G1 G42 X0 Y{Math.Round(((y / 3) + 3), 2)}"); // 2.1
                    gcodeBuilder.AppendLine("N20 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне DC");
                    gcodeBuilder.AppendLine("N21 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (2/3)");
                    gcodeBuilder.AppendLine("N22 M10()");
                    gcodeBuilder.AppendLine($"N23 G1 G42 X0 Y{Math.Round((y / 3), 2)}");
                    gcodeBuilder.AppendLine("N24 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне DC");
                    gcodeBuilder.AppendLine("N25 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (3/3)");
                    gcodeBuilder.AppendLine("N26 M10()");
                    gcodeBuilder.AppendLine($"N27 G1 G42 X0 Y{Math.Round(((y / 3) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N28 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами DC и CB");
                    gcodeBuilder.AppendLine("N29 G0 G42 X3 Y0.25");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (1/3)");
                    gcodeBuilder.AppendLine("N30 M10()");
                    gcodeBuilder.AppendLine($"N31 G1 G42 X-{Math.Round(((x / 3) + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N32 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне CB");
                    gcodeBuilder.AppendLine("N33 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (2/3)");
                    gcodeBuilder.AppendLine("N34 M10()");
                    gcodeBuilder.AppendLine($"N35 G1 G42 X-{Math.Round((x / 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N36 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне CB");
                    gcodeBuilder.AppendLine("N37 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (3/3)");
                    gcodeBuilder.AppendLine("N38 M10()");
                    gcodeBuilder.AppendLine($"N39 G1 G42 X-{Math.Round(((x / 3) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N40 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами CB и BA");
                    gcodeBuilder.AppendLine("N41 G0 G42 X-0.25 Y3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (1/3)");
                    gcodeBuilder.AppendLine("N42 M10()");
                    gcodeBuilder.AppendLine($"N43 G1 G42 X0 Y-{Math.Round(((y / 3) + 3), 2)}");
                    gcodeBuilder.AppendLine("N44 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне BA");
                    gcodeBuilder.AppendLine("N45 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (2/3)");
                    gcodeBuilder.AppendLine("N46 M10()");
                    gcodeBuilder.AppendLine($"N47 G1 G42 X0 Y-{Math.Round((y / 3), 2)}");
                    gcodeBuilder.AppendLine("N48 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне BA");
                    gcodeBuilder.AppendLine("N49 G0 G42 X0 Y-0.3");
                    gcodeBuilder.AppendLine("N50 M10()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (3/3)");
                    gcodeBuilder.AppendLine($"N51 G1 G42 X0 Y-{Math.Round(((y / 3) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N52 M11()");
                    gcodeBuilder.AppendLine("");

                    gcodeBuilder.AppendLine("; Конец программы");
                    gcodeBuilder.AppendLine("N53 M30");

                    GCodeTextBox.Text = gcodeBuilder.ToString().Replace(",", ".");
                    break;

                case 3:

                    gcodeBuilder.AppendLine($"; X = {x}, Y = {y}, кол-во перемычек на каждой стороне = 3, длина перемычек = 0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Начало программы");
                    gcodeBuilder.AppendLine("%");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к абсолютным координатам и перемещение на начало координат");
                    gcodeBuilder.AppendLine("N01 G90");
                    gcodeBuilder.AppendLine("N02 G54");
                    gcodeBuilder.AppendLine("N03 M13()");
                    gcodeBuilder.AppendLine("N04 G0 G42 X-3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к относительным координатам и построение сторон детали");
                    gcodeBuilder.AppendLine("N05 G91");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (1/4)");
                    gcodeBuilder.AppendLine("N06 M10()");
                    gcodeBuilder.AppendLine($"N07 G1 G42 X{Math.Round((x / 4 + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N08 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне AD");
                    gcodeBuilder.AppendLine("N09 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (2/4)");
                    gcodeBuilder.AppendLine("N10 M10()");
                    gcodeBuilder.AppendLine($"N11 G1 G42 X{Math.Round((x / 4), 2)} Y0");
                    gcodeBuilder.AppendLine("N12 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне AD");
                    gcodeBuilder.AppendLine("N13 G0 G42 X0.3 Y0");
                    
                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (3/4)");
                    gcodeBuilder.AppendLine("N14 M10()");
                    gcodeBuilder.AppendLine($"N15 G1 G42 X{Math.Round((x / 4), 2)} Y0");
                    gcodeBuilder.AppendLine("N16 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне AD");
                    gcodeBuilder.AppendLine("N17 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (4/4)");
                    gcodeBuilder.AppendLine("N18 M10()");
                    gcodeBuilder.AppendLine($"N19 G1 G42 X{Math.Round(((x / 4) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N20 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами AD и DC");
                    gcodeBuilder.AppendLine("N21 G0 G42 X0.25 Y-3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (1/4)");
                    gcodeBuilder.AppendLine("N22 M10()");
                    gcodeBuilder.AppendLine($"N23 G1 G42 X0 Y{Math.Round(((y / 4) + 3), 2)}");
                    gcodeBuilder.AppendLine("N24 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне DC");
                    gcodeBuilder.AppendLine("N25 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (2/4)");
                    gcodeBuilder.AppendLine("N26 M10()");
                    gcodeBuilder.AppendLine($"N27 G1 G42 X0 Y{Math.Round((y / 4), 2)}");
                    gcodeBuilder.AppendLine("N28 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне DC");
                    gcodeBuilder.AppendLine("N29 G0 G42 X0 Y0.3");
                    
                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (3/4)");
                    gcodeBuilder.AppendLine("N30 M10()");
                    gcodeBuilder.AppendLine($"N31 G1 G42 X0 Y{Math.Round((y / 4), 2)}");
                    gcodeBuilder.AppendLine("N32 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне DC");
                    gcodeBuilder.AppendLine("N33 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (4/4)");
                    gcodeBuilder.AppendLine("N34 M10()");
                    gcodeBuilder.AppendLine($"N35 G1 G42 X0 Y{Math.Round(((y / 4) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N36 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами DC и CB");
                    gcodeBuilder.AppendLine("N37 G0 G42 X3 Y0.25");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (1/4)");
                    gcodeBuilder.AppendLine("N38 M10()");
                    gcodeBuilder.AppendLine($"N39 G1 G42 X-{Math.Round(((x / 4) + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N40 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне CB");
                    gcodeBuilder.AppendLine("N41 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (2/4)");
                    gcodeBuilder.AppendLine("N42 M10()");
                    gcodeBuilder.AppendLine($"N43 G1 G42 X-{Math.Round((x / 4), 2)} Y0");
                    gcodeBuilder.AppendLine("N44 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне CB");
                    gcodeBuilder.AppendLine("N45 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (3/4)");
                    gcodeBuilder.AppendLine("N46 M10()");
                    gcodeBuilder.AppendLine($"N47 G1 G42 X-{Math.Round((x / 4), 2)} Y0");
                    gcodeBuilder.AppendLine("N48 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне CB");
                    gcodeBuilder.AppendLine("N49 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (4/4)");
                    gcodeBuilder.AppendLine("N50 M10()");
                    gcodeBuilder.AppendLine($"N51 G1 G42 X-{Math.Round(((x / 4) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N52 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами CB и BA");
                    gcodeBuilder.AppendLine("N53 G0 G42 X-0.25 Y3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (1/4)");
                    gcodeBuilder.AppendLine("N54 M10()");
                    gcodeBuilder.AppendLine($"N55 G1 G42 X0 Y-{Math.Round(((y / 4) + 3), 2)}");
                    gcodeBuilder.AppendLine("N56 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне BA");
                    gcodeBuilder.AppendLine("N57 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (2/4)");
                    gcodeBuilder.AppendLine("N58 M10()");
                    gcodeBuilder.AppendLine($"N59 G1 G42 X0 Y-{Math.Round((y / 4), 2)}");
                    gcodeBuilder.AppendLine("N60 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне BA");
                    gcodeBuilder.AppendLine("N61 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (3/4)");
                    gcodeBuilder.AppendLine("N62 M10()");
                    gcodeBuilder.AppendLine($"N63 G1 G42 X0 Y-{Math.Round((y / 4), 2)}");
                    gcodeBuilder.AppendLine("N64 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне BA");
                    gcodeBuilder.AppendLine("N65 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (4/4)");
                    gcodeBuilder.AppendLine("N66 M10()");
                    gcodeBuilder.AppendLine($"N67 G1 G42 X0 Y-{Math.Round(((y / 4) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N68 M11()");
                    gcodeBuilder.AppendLine("");

                    gcodeBuilder.AppendLine("; Конец программы");
                    gcodeBuilder.AppendLine("N69 M30");

                    GCodeTextBox.Text = gcodeBuilder.ToString().Replace(",", ".");
                    break;

                case 4:

                    gcodeBuilder.AppendLine($"; X = {x}, Y = {y}, кол-во перемычек на каждой стороне = 4, длина перемычек = 0.3");
                    
                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Начало программы");
                    gcodeBuilder.AppendLine("%");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к абсолютным координатам и перемещение на начало координат");
                    gcodeBuilder.AppendLine("N01 G90");
                    gcodeBuilder.AppendLine("N02 G54");
                    gcodeBuilder.AppendLine("N03 M13()");
                    gcodeBuilder.AppendLine("N04 G0 G42 X-1 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к относительным координатам и построение сторон детали");
                    gcodeBuilder.AppendLine("N05 G91");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (1/5)");
                    gcodeBuilder.AppendLine("N06 M10()");
                    gcodeBuilder.AppendLine($"N07 G1 G42 X{Math.Round((x / 5 + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N08 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне AD");
                    gcodeBuilder.AppendLine("N09 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (2/5)");
                    gcodeBuilder.AppendLine("N10 M10()");
                    gcodeBuilder.AppendLine($"N11 G1 G42 X{Math.Round((x / 5), 2)} Y0");
                    gcodeBuilder.AppendLine("N12 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне AD");
                    gcodeBuilder.AppendLine("N13 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (3/5)");
                    gcodeBuilder.AppendLine("N14 M10()");
                    gcodeBuilder.AppendLine($"N15 G1 G42 X{Math.Round((x / 5), 2)} Y0"); 
                    gcodeBuilder.AppendLine("N16 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне AD");
                    gcodeBuilder.AppendLine("N17 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (4/5)");
                    gcodeBuilder.AppendLine("N18 M10()");
                    gcodeBuilder.AppendLine($"N19 G1 G42 X{Math.Round((x / 5), 2)} Y0");
                    gcodeBuilder.AppendLine("N20 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне AD");
                    gcodeBuilder.AppendLine("N21 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (5/5)");
                    gcodeBuilder.AppendLine("N22 M10()");
                    gcodeBuilder.AppendLine($"N23 G1 G42 X{Math.Round(((x / 5) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N24 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами AD и DC");
                    gcodeBuilder.AppendLine("N25 G0 G42 X0.25 Y-3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (1/5)");
                    gcodeBuilder.AppendLine("N26 M10()");
                    gcodeBuilder.AppendLine($"N27 G1 G42 X0 Y{Math.Round(((y / 5) + 3), 2)}");
                    gcodeBuilder.AppendLine("N28 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне DC");
                    gcodeBuilder.AppendLine("N29 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (2/5)");
                    gcodeBuilder.AppendLine("N30 M10()");
                    gcodeBuilder.AppendLine($"N31 G1 G42 X0 Y{Math.Round((y / 5)), 2}");
                    gcodeBuilder.AppendLine("N32 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне DC");
                    gcodeBuilder.AppendLine("N33 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (3/5)");
                    gcodeBuilder.AppendLine("N34 M10()");
                    gcodeBuilder.AppendLine($"N35 G1 G42 X0 Y{Math.Round((y / 5), 2)}");
                    gcodeBuilder.AppendLine("N36 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне DC");
                    gcodeBuilder.AppendLine("N37 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (4/5)");
                    gcodeBuilder.AppendLine("N38 M10()");
                    gcodeBuilder.AppendLine($"N39 G1 G42 X0 Y{Math.Round((y / 5), 2)}");
                    gcodeBuilder.AppendLine("N40 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне DC");
                    gcodeBuilder.AppendLine("N41 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (5/5)");
                    gcodeBuilder.AppendLine("N42 M10()");
                    gcodeBuilder.AppendLine($"N43 G1 G42 X0 Y{Math.Round(((y / 5) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N44 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами DC и CB");
                    gcodeBuilder.AppendLine("N45 G0 G42 X3 Y0.25");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (1/5)");
                    gcodeBuilder.AppendLine("N46 M10()");
                    gcodeBuilder.AppendLine($"N47 G1 G42 X-{Math.Round(((x / 5) + 5), 2)} Y0");
                    gcodeBuilder.AppendLine("N48 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне CB");
                    gcodeBuilder.AppendLine("N49 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (2/5)");
                    gcodeBuilder.AppendLine("N50 M10()");
                    gcodeBuilder.AppendLine($"N51 G1 G42 X-{Math.Round(((x / 5)), 2)} Y0");
                    gcodeBuilder.AppendLine("N52 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне CB");
                    gcodeBuilder.AppendLine("N53 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (3/5)");
                    gcodeBuilder.AppendLine("N54 M10()");
                    gcodeBuilder.AppendLine($"N55 G1 G42 X-{Math.Round((x / 5), 2)} Y0");
                    gcodeBuilder.AppendLine("N56 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне CB");
                    gcodeBuilder.AppendLine("N57 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (4/5)");
                    gcodeBuilder.AppendLine("N58 M10()");
                    gcodeBuilder.AppendLine($"N59 G1 G42 X-{Math.Round(((x / 5)), 2)} Y0");
                    gcodeBuilder.AppendLine("N60 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне CB");
                    gcodeBuilder.AppendLine("N61 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (5/5)");
                    gcodeBuilder.AppendLine("N62 M10()");
                    gcodeBuilder.AppendLine($"N63 G1 G42 X-{Math.Round(((x / 5) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N64 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами CB и BA");
                    gcodeBuilder.AppendLine("N65 G0 G42 X-0.25 Y3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (1/5)");
                    gcodeBuilder.AppendLine("N66 M10()");
                    gcodeBuilder.AppendLine($"N67 G1 G42 X0 Y-{Math.Round(((y / 5) + 3), 2)}");
                    gcodeBuilder.AppendLine("N68 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне BA");
                    gcodeBuilder.AppendLine("N69 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (2/5)");
                    gcodeBuilder.AppendLine("N70 M10()");
                    gcodeBuilder.AppendLine($"N71 G1 G42 X0 Y-{Math.Round((y / 5), 2)}");
                    gcodeBuilder.AppendLine("N72 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне BA");
                    gcodeBuilder.AppendLine("N73 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (3/5)");
                    gcodeBuilder.AppendLine("N74 M10()");
                    gcodeBuilder.AppendLine($"N75 G1 G42 X0 Y-{Math.Round((y / 5), 2)}");
                    gcodeBuilder.AppendLine("N76 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне BA");
                    gcodeBuilder.AppendLine("N77 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (4/5)");
                    gcodeBuilder.AppendLine("N78 M10()");
                    gcodeBuilder.AppendLine($"N79 G1 G42 X0 Y-{Math.Round((y / 5), 2)}");
                    gcodeBuilder.AppendLine("N80 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне BA");
                    gcodeBuilder.AppendLine("N81 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (5/5)");
                    gcodeBuilder.AppendLine("N82 M10()");
                    gcodeBuilder.AppendLine($"N83 G1 G42 X0 Y-{Math.Round(((y / 5) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N84 M11()");
                    gcodeBuilder.AppendLine("");

                    gcodeBuilder.AppendLine("; Конец программы");
                    gcodeBuilder.AppendLine("N85 M30");

                    GCodeTextBox.Text = gcodeBuilder.ToString().Replace(",", ".");
                    break;

                case 5:

                    gcodeBuilder.AppendLine($"; X = {x}, Y = {y}, кол-во перемычек на каждой стороне = 5, длина перемычек = 0.3");
                    
                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Начало программы");
                    gcodeBuilder.AppendLine("%");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к абсолютным координатам и перемещение на начало координат");
                    gcodeBuilder.AppendLine("N01 G90");
                    gcodeBuilder.AppendLine("N02 G54");
                    gcodeBuilder.AppendLine("N03 M13()");
                    gcodeBuilder.AppendLine("N04 G0 G42 X-1 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к относительным координатам и построение сторон детали");
                    gcodeBuilder.AppendLine("N05 G91");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (1/6)");
                    gcodeBuilder.AppendLine("N06 M10()");
                    gcodeBuilder.AppendLine($"N07 G1 G42 X{Math.Round((x / 6 + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N08 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне AD");
                    gcodeBuilder.AppendLine("N09 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (2/6)");
                    gcodeBuilder.AppendLine("N10 M10()");
                    gcodeBuilder.AppendLine($"N11 G1 G42 X{Math.Round((x / 6), 2)} Y0");
                    gcodeBuilder.AppendLine("N12 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне AD");
                    gcodeBuilder.AppendLine("N13 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (3/6)");
                    gcodeBuilder.AppendLine("N14 M10()");
                    gcodeBuilder.AppendLine($"N15 G1 G42 X{Math.Round((x / 6), 2)} Y0");
                    gcodeBuilder.AppendLine("N16 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне AD");
                    gcodeBuilder.AppendLine("N17 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (4/6)");
                    gcodeBuilder.AppendLine("N18 M10()");
                    gcodeBuilder.AppendLine($"N19 G1 G42 X{Math.Round((x / 6), 2)} Y0");
                    gcodeBuilder.AppendLine("N20 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне AD");
                    gcodeBuilder.AppendLine("N21 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (5/6)");
                    gcodeBuilder.AppendLine("N22 M10()");
                    gcodeBuilder.AppendLine($"N23 G1 G42 X{Math.Round((x / 6), 2)} Y0");
                    gcodeBuilder.AppendLine("N24 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне AD");
                    gcodeBuilder.AppendLine("N25 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (6/6)");
                    gcodeBuilder.AppendLine("N26 M10()");
                    gcodeBuilder.AppendLine($"N27 G1 G42 X{Math.Round(((x / 6) - 0.55 * selectedNumber),2)} Y0");
                    gcodeBuilder.AppendLine("N28 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами AD и DC");
                    gcodeBuilder.AppendLine("N29 G0 G42 X0.25 Y-3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (1/6)");
                    gcodeBuilder.AppendLine("N30 M10()");
                    gcodeBuilder.AppendLine($"N31 G1 G42 X0 Y{Math.Round(((y / 6) + 3), 2)}");
                    gcodeBuilder.AppendLine("N32 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне DC");
                    gcodeBuilder.AppendLine("N33 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (2/6)");
                    gcodeBuilder.AppendLine("N34 M10()");
                    gcodeBuilder.AppendLine($"N35 G1 G42 X0 Y{Math.Round((y / 6), 2)}");
                    gcodeBuilder.AppendLine("N36 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне DC");
                    gcodeBuilder.AppendLine("N37 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (3/6)");
                    gcodeBuilder.AppendLine("N38 M10()");
                    gcodeBuilder.AppendLine($"N39 G1 G42 X0 Y{Math.Round((y / 6), 2)}");
                    gcodeBuilder.AppendLine("N40 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне DC");
                    gcodeBuilder.AppendLine("N41 G0 G42 X0 Y0.3");
                    gcodeBuilder.AppendLine("N42 M10()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (4/6)");
                    gcodeBuilder.AppendLine($"N43 G1 G42 X0 Y{Math.Round((y / 6), 2)}");
                    gcodeBuilder.AppendLine("N44 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне DC");
                    gcodeBuilder.AppendLine("N45 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (5/6)");
                    gcodeBuilder.AppendLine("N46 M10()");
                    gcodeBuilder.AppendLine($"N47 G1 G42 X0 Y{Math.Round((y / 6), 2)}");
                    gcodeBuilder.AppendLine("N48 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне DC");
                    gcodeBuilder.AppendLine("N49 G0 ч   G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (6/6)");
                    gcodeBuilder.AppendLine("N50 M10()");
                    gcodeBuilder.AppendLine($"N51 G1 G42 X0 Y{Math.Round(((y / 6) - 0.55 * selectedNumber), 2)}"); // 2.6
                    gcodeBuilder.AppendLine("N52 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами DC и CB");
                    gcodeBuilder.AppendLine("N53 G0 G42 X3 Y0.25");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (1/6)");
                    gcodeBuilder.AppendLine("N54 M10()");
                    gcodeBuilder.AppendLine($"N55 G1 G42 X-{Math.Round(((x / 6) + 5), 2)} Y0");
                    gcodeBuilder.AppendLine("N56 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне CB");
                    gcodeBuilder.AppendLine("N57 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (2/6)");
                    gcodeBuilder.AppendLine("N58 M10()");
                    gcodeBuilder.AppendLine($"N59 G1 G42 X-{Math.Round(((x / 6)), 2)} Y0");
                    gcodeBuilder.AppendLine("N60 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне CB");
                    gcodeBuilder.AppendLine("N61 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (3/6)");
                    gcodeBuilder.AppendLine("N62 M10()");
                    gcodeBuilder.AppendLine($"N63 G1 G42 X-{Math.Round((x / 6), 2)} Y0");
                    gcodeBuilder.AppendLine("N64 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне CB");
                    gcodeBuilder.AppendLine("N65 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (4/6)");
                    gcodeBuilder.AppendLine("N66 M10()");
                    gcodeBuilder.AppendLine($"N67 G1 G42 X-{Math.Round((x / 6), 2)} Y0");
                    gcodeBuilder.AppendLine("N68 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне CB");
                    gcodeBuilder.AppendLine("N69 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (5/6)");
                    gcodeBuilder.AppendLine("N70 M10()");
                    gcodeBuilder.AppendLine($"N71 G1 G42 X-{Math.Round((x / 6), 2)} Y0");
                    gcodeBuilder.AppendLine("N72 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне CB");
                    gcodeBuilder.AppendLine("N73 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (6/6)");
                    gcodeBuilder.AppendLine("N74 M10()");
                    gcodeBuilder.AppendLine($"N75 G1 G42 X-{Math.Round(((x / 6) - 0.55 * selectedNumber),2)} Y0");
                    gcodeBuilder.AppendLine("N76 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами CB и BA");
                    gcodeBuilder.AppendLine("N77 G0 G42 X-0.25 Y3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (1/6)");
                    gcodeBuilder.AppendLine("N78 M10()");
                    gcodeBuilder.AppendLine($"N79 G1 G42 X0 Y-{Math.Round(((y / 6) + 3), 2)}");
                    gcodeBuilder.AppendLine("N80 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне BA");
                    gcodeBuilder.AppendLine("N81 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (2/6)");
                    gcodeBuilder.AppendLine("N82 M10()");
                    gcodeBuilder.AppendLine($"N83 G1 G42 X0 Y-{Math.Round((y / 6), 2)}");
                    gcodeBuilder.AppendLine("N84 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне BA");
                    gcodeBuilder.AppendLine("N85 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (3/6)");
                    gcodeBuilder.AppendLine("N86 M10()");
                    gcodeBuilder.AppendLine($"N87 G1 G42 X0 Y-{Math.Round((y / 6), 2)}");
                    gcodeBuilder.AppendLine("N88 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне BA");
                    gcodeBuilder.AppendLine("N89 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (4/6)");
                    gcodeBuilder.AppendLine("N90 M10()");
                    gcodeBuilder.AppendLine($"N91 G1 G42 X0 Y-{Math.Round((y / 6), 2)}");
                    gcodeBuilder.AppendLine("N92 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне BA");
                    gcodeBuilder.AppendLine("N93 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (5/6)");
                    gcodeBuilder.AppendLine("N94 M10()");
                    gcodeBuilder.AppendLine($"N95 G1 G42 X0 Y-{Math.Round((y / 6), 2)}");
                    gcodeBuilder.AppendLine("N96 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне BA");
                    gcodeBuilder.AppendLine("N97 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (6/6)");
                    gcodeBuilder.AppendLine("N98 M10()");
                    gcodeBuilder.AppendLine($"N99 G1 G42 X0 Y-{Math.Round(((y / 6) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N100 M11()");
                    gcodeBuilder.AppendLine("");

                    gcodeBuilder.AppendLine("; Конец программы");
                    gcodeBuilder.AppendLine("N101 M30");

                    GCodeTextBox.Text = gcodeBuilder.ToString().Replace(",", ".");
                    break;

                case 6:

                    gcodeBuilder.AppendLine($"; X = {x}, Y = {y}, кол-во перемычек на каждой стороне = 6, длина перемычек = 0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Начало программы");
                    gcodeBuilder.AppendLine("%");
                    gcodeBuilder.AppendLine("");

                    gcodeBuilder.AppendLine("; Переход к абсолютным координатам и перемещение на начало координат");
                    gcodeBuilder.AppendLine("N01 G90");
                    gcodeBuilder.AppendLine("N02 G54");
                    gcodeBuilder.AppendLine("N03 M13()");
                    gcodeBuilder.AppendLine("N04 G0 G42 X-1 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к относительным координатам и построение сторон детали");
                    gcodeBuilder.AppendLine("N05 G91");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (1/7)");
                    gcodeBuilder.AppendLine("N06 M10()");
                    gcodeBuilder.AppendLine($"N07 G1 G42 X{Math.Round((x / 7 + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N08 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне AD");
                    gcodeBuilder.AppendLine("N09 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (2/7)");
                    gcodeBuilder.AppendLine("N10 M10()");
                    gcodeBuilder.AppendLine($"N11 G1 G42 X{Math.Round((x / 7), 2)} Y0");
                    gcodeBuilder.AppendLine("N12 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне AD");
                    gcodeBuilder.AppendLine("N13 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (3/7)");
                    gcodeBuilder.AppendLine("N14 M10()");
                    gcodeBuilder.AppendLine($"N15 G1 G42 X{Math.Round((x / 7), 2)} Y0");
                    gcodeBuilder.AppendLine("N16 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне AD");
                    gcodeBuilder.AppendLine("N17 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (4/7)");
                    gcodeBuilder.AppendLine("N18 M10()");
                    gcodeBuilder.AppendLine($"N19 G1 G42 X{Math.Round((x / 7), 2)} Y0");
                    gcodeBuilder.AppendLine("N20 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне AD");
                    gcodeBuilder.AppendLine("N21 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (5/7)");
                    gcodeBuilder.AppendLine("N22 M10()");
                    gcodeBuilder.AppendLine($"N23 G1 G42 X{Math.Round((x / 7), 2)} Y0");
                    gcodeBuilder.AppendLine("N24 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне AD");
                    gcodeBuilder.AppendLine("N25 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (6/7)");
                    gcodeBuilder.AppendLine("N26 M10()");
                    gcodeBuilder.AppendLine($"N27 G1 G42 X{Math.Round((x / 7), 2)} Y0");
                    gcodeBuilder.AppendLine("N28 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне AD");
                    gcodeBuilder.AppendLine("N29 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (7/7)");
                    gcodeBuilder.AppendLine("N30 M10()");
                    gcodeBuilder.AppendLine($"N31 G1 G42 X{Math.Round(((x / 7) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N32 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами AD и DC");
                    gcodeBuilder.AppendLine("N33 G0 G42 X0.25 Y-3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (1/7)");
                    gcodeBuilder.AppendLine("N34 M10()");
                    gcodeBuilder.AppendLine($"N35 G1 G42 X0 Y{Math.Round(((y / 7) + 3), 2)}");
                    gcodeBuilder.AppendLine("N36 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне DC");
                    gcodeBuilder.AppendLine("N37 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (2/7)");
                    gcodeBuilder.AppendLine("N38 M10()");
                    gcodeBuilder.AppendLine($"N39 G1 G42 X0 Y{Math.Round((y / 7), 2)}");
                    gcodeBuilder.AppendLine("N40 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне DC");
                    gcodeBuilder.AppendLine("N41 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (3/7)");
                    gcodeBuilder.AppendLine("N42 M10()");
                    gcodeBuilder.AppendLine($"N43 G1 G42 X0 Y{Math.Round((y / 7), 2)}");
                    gcodeBuilder.AppendLine("N44 M11()");
                    
                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне DC");
                    gcodeBuilder.AppendLine("N45 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (4/7)");
                    gcodeBuilder.AppendLine("N46 M10()");
                    gcodeBuilder.AppendLine($"N47 G1 G42 X0 Y{Math.Round((y / 7), 2)}");
                    gcodeBuilder.AppendLine("N48 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне DC");
                    gcodeBuilder.AppendLine("N49 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (5/7)");
                    gcodeBuilder.AppendLine("N50 M10()");
                    gcodeBuilder.AppendLine($"N51 G1 G42 X0 Y{Math.Round((y / 7), 2)}");
                    gcodeBuilder.AppendLine("N52 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне DC");
                    gcodeBuilder.AppendLine("N53 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (6/7)");
                    gcodeBuilder.AppendLine("N54 M10()");
                    gcodeBuilder.AppendLine($"N55 G1 G42 X0 Y{Math.Round((y / 7), 2)}");
                    gcodeBuilder.AppendLine("N56 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне DC");
                    gcodeBuilder.AppendLine("N57 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (7/7)");
                    gcodeBuilder.AppendLine("N58 M10()");
                    gcodeBuilder.AppendLine($"N59 G1 G42 X0 Y{Math.Round(((y / 7) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N60 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами DC и CB");
                    gcodeBuilder.AppendLine("N61 G0 G42 X3 Y0.25");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (1/7)");
                    gcodeBuilder.AppendLine("N62 M10()");
                    gcodeBuilder.AppendLine($"N63 G1 G42 X-{Math.Round(((x / 7) + 5), 2)} Y0");
                    gcodeBuilder.AppendLine("N64 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне CB");
                    gcodeBuilder.AppendLine("N65 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (2/7)");
                    gcodeBuilder.AppendLine("N66 M10()");
                    gcodeBuilder.AppendLine($"N67 G1 G42 X-{Math.Round((x / 7), 2)} Y0");
                    gcodeBuilder.AppendLine("N68 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне CB");
                    gcodeBuilder.AppendLine("N69 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (3/7)");
                    gcodeBuilder.AppendLine("N70 M10()");
                    gcodeBuilder.AppendLine($"N71 G1 G42 X-{Math.Round((x / 7), 2)} Y0");
                    gcodeBuilder.AppendLine("N72 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне CB");
                    gcodeBuilder.AppendLine("N73 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (4/7)");
                    gcodeBuilder.AppendLine("N74 M10()");
                    gcodeBuilder.AppendLine($"N75 G1 G42 X-{Math.Round((x / 7), 2)} Y0");
                    gcodeBuilder.AppendLine("N76 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне CB");
                    gcodeBuilder.AppendLine("N77 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (5/7)");
                    gcodeBuilder.AppendLine("N78 M10()");
                    gcodeBuilder.AppendLine($"N79 G1 G42 X-{Math.Round((x / 7), 2)} Y0");
                    gcodeBuilder.AppendLine("N80 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне CB");
                    gcodeBuilder.AppendLine("N81 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (6/7)");
                    gcodeBuilder.AppendLine("N82 M10()");
                    gcodeBuilder.AppendLine($"N83 G1 G42 X-{Math.Round((x / 7), 2)} Y0");
                    gcodeBuilder.AppendLine("N84 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне CB");
                    gcodeBuilder.AppendLine("N85 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (7/7)");
                    gcodeBuilder.AppendLine("N86 M10()");
                    gcodeBuilder.AppendLine($"N87 G1 G42 X-{Math.Round(((x / 7) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N88 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами CB и BA");
                    gcodeBuilder.AppendLine("N89 G0 G42 X-0.25 Y3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (1/7)");
                    gcodeBuilder.AppendLine("N90 M10()");
                    gcodeBuilder.AppendLine($"N91 G1 G42 X0 Y-{Math.Round(((y / 7) + 3), 2)}");
                    gcodeBuilder.AppendLine("N92 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне BA");
                    gcodeBuilder.AppendLine("N93 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (2/7)");
                    gcodeBuilder.AppendLine("N94 M10()");
                    gcodeBuilder.AppendLine($"N95 G1 G42 X0 Y-{Math.Round((y / 7), 2)}");
                    gcodeBuilder.AppendLine("N96 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне BA");
                    gcodeBuilder.AppendLine("N97 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (3/7)");
                    gcodeBuilder.AppendLine("N98 M10()");
                    gcodeBuilder.AppendLine($"N99 G1 G42 X0 Y-{Math.Round((y / 7), 2)}");
                    gcodeBuilder.AppendLine("N100 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне BA");
                    gcodeBuilder.AppendLine("N101 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (4/7)");
                    gcodeBuilder.AppendLine("N102 M10()");
                    gcodeBuilder.AppendLine($"N103 G1 G42 X0 Y-{Math.Round((y / 7), 2)}");
                    gcodeBuilder.AppendLine("N104 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне BA");
                    gcodeBuilder.AppendLine("N105 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (5/7)");
                    gcodeBuilder.AppendLine("N106 M10()");
                    gcodeBuilder.AppendLine($"N107 G1 G42 X0 Y-{Math.Round((y / 7), 2)}");
                    gcodeBuilder.AppendLine("N108 M11()");

                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне BA");
                    gcodeBuilder.AppendLine("N109 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (6/7)");
                    gcodeBuilder.AppendLine("N110 M10()");
                    gcodeBuilder.AppendLine($"N111 G1 G42 X0 Y-{Math.Round((y / 7), 2)}");
                    gcodeBuilder.AppendLine("N112 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне BA");
                    gcodeBuilder.AppendLine("N113 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (7/7)");
                    gcodeBuilder.AppendLine("N114 M10()");
                    gcodeBuilder.AppendLine($"N115 G1 G42 X0 Y-{Math.Round(((y / 7) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N116 M11()");
                    gcodeBuilder.AppendLine("");

                    gcodeBuilder.AppendLine("; Конец программы");
                    gcodeBuilder.AppendLine("N117 M30");

                    GCodeTextBox.Text = gcodeBuilder.ToString().Replace(",", ".");
                    break;

                case 7:

                    gcodeBuilder.AppendLine($"; X = {x}, Y = {y}, кол-во перемычек на каждой стороне = 7, длина перемычек = 0.3");
                    
                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Начало программы");
                    gcodeBuilder.AppendLine("%");
                    gcodeBuilder.AppendLine("");

                    gcodeBuilder.AppendLine("; Переход к абсолютным координатам и перемещение на начало координат");
                    gcodeBuilder.AppendLine("N01 G90");
                    gcodeBuilder.AppendLine("N02 G54");
                    gcodeBuilder.AppendLine("N03 M13()");
                    gcodeBuilder.AppendLine("N04 G0 G42 X-1 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к относительным координатам и построение сторон детали");
                    gcodeBuilder.AppendLine("N05 G91");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (1/8)");
                    gcodeBuilder.AppendLine("N06 M10()");
                    gcodeBuilder.AppendLine($"N07 G1 G42 X{Math.Round((x / 8 + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N08 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне AD");
                    gcodeBuilder.AppendLine("N09 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (2/8)");
                    gcodeBuilder.AppendLine("N10 M10()");
                    gcodeBuilder.AppendLine($"N11 G1 G42 X{Math.Round((x / 8), 2)} Y0");
                    gcodeBuilder.AppendLine("N12 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне AD");
                    gcodeBuilder.AppendLine("N13 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (3/8)");
                    gcodeBuilder.AppendLine("N14 M10()");
                    gcodeBuilder.AppendLine($"N15 G1 G42 X{Math.Round((x / 8), 2)} Y0");
                    gcodeBuilder.AppendLine("N16 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне AD");
                    gcodeBuilder.AppendLine("N17 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (4/8)");
                    gcodeBuilder.AppendLine("N18 M10()");
                    gcodeBuilder.AppendLine($"N19 G1 G42 X{Math.Round((x / 8), 2)} Y0");
                    gcodeBuilder.AppendLine("N20 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне AD");
                    gcodeBuilder.AppendLine("N21 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (5/8)");
                    gcodeBuilder.AppendLine("N22 M10()");
                    gcodeBuilder.AppendLine($"N23 G1 G42 X{Math.Round((x / 8), 2)} Y0"); 
                    gcodeBuilder.AppendLine("N24 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне AD");
                    gcodeBuilder.AppendLine("N25 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (6/8)");
                    gcodeBuilder.AppendLine("N26 M10()");
                    gcodeBuilder.AppendLine($"N27 G1 G42 X{Math.Round((x / 8), 2)} Y0");
                    gcodeBuilder.AppendLine("N28 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне AD");
                    gcodeBuilder.AppendLine("N29 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (7/8)");
                    gcodeBuilder.AppendLine("N30 M10()");
                    gcodeBuilder.AppendLine($"N31 G1 G42 X{Math.Round((x / 8), 2)} Y0");
                    gcodeBuilder.AppendLine("N32 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне AD");
                    gcodeBuilder.AppendLine("N33 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (8/8)");
                    gcodeBuilder.AppendLine("N34 M10()");
                    gcodeBuilder.AppendLine($"N35 G1 G42 X{Math.Round(((x / 8) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N36 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами AD и DC");
                    gcodeBuilder.AppendLine("N37 G0 G42 X0.25 Y-3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (1/8)");
                    gcodeBuilder.AppendLine("N38 M10()");
                    gcodeBuilder.AppendLine($"N39 G1 G42 X0 Y{Math.Round(((y / 8) + 3), 2)}");
                    gcodeBuilder.AppendLine("N40 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне DC");
                    gcodeBuilder.AppendLine("N41 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (2/8)");
                    gcodeBuilder.AppendLine("N42 M10()");
                    gcodeBuilder.AppendLine($"N43 G1 G42 X0 Y{Math.Round((y / 8), 2)}");
                    gcodeBuilder.AppendLine("N44 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне DC");
                    gcodeBuilder.AppendLine("N45 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (3/8)");
                    gcodeBuilder.AppendLine("N46 M10()");
                    gcodeBuilder.AppendLine($"N47 G1 G42 X0 Y{Math.Round((y / 8), 2)}");
                    gcodeBuilder.AppendLine("N48 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне DC");
                    gcodeBuilder.AppendLine("N49 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (4/8)");
                    gcodeBuilder.AppendLine("N50 M10()");
                    gcodeBuilder.AppendLine($"N51 G1 G42 X0 Y{Math.Round((y / 8), 2)}");
                    gcodeBuilder.AppendLine("N52 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне DC");
                    gcodeBuilder.AppendLine("N53 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (5/8)");
                    gcodeBuilder.AppendLine("N54 M10()");
                    gcodeBuilder.AppendLine($"N55 G1 G42 X0 Y{Math.Round((y / 8), 2)}");
                    gcodeBuilder.AppendLine("N56 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне DC");
                    gcodeBuilder.AppendLine("N57 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (6/8)");
                    gcodeBuilder.AppendLine("N58 M10()");
                    gcodeBuilder.AppendLine($"N59 G1 G42 X0 Y{Math.Round((y / 8), 2)}");
                    gcodeBuilder.AppendLine("N60 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне DC");
                    gcodeBuilder.AppendLine("N61 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (7/8)");
                    gcodeBuilder.AppendLine("N62 M10()");
                    gcodeBuilder.AppendLine($"N63 G1 G42 X0 Y{Math.Round((y / 8), 2)}");
                    gcodeBuilder.AppendLine("N64 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне DC");
                    gcodeBuilder.AppendLine("N65 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (8/8)");
                    gcodeBuilder.AppendLine("N66 M10()");
                    gcodeBuilder.AppendLine($"N67 G1 G42 X0 Y{Math.Round(((y / 8) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N68 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами DC и CB");
                    gcodeBuilder.AppendLine("N69 G0 G42 X3 Y0.25");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (1/8)");
                    gcodeBuilder.AppendLine("N70 M10()");
                    gcodeBuilder.AppendLine($"N71 G1 G42 X-{Math.Round(((x / 8) + 5), 2)} Y0");
                    gcodeBuilder.AppendLine("N72 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне CB");
                    gcodeBuilder.AppendLine("N73 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (2/8)");
                    gcodeBuilder.AppendLine("N74 M10()");
                    gcodeBuilder.AppendLine($"N75 G1 G42 X-{Math.Round((x / 8), 2)} Y0");
                    gcodeBuilder.AppendLine("N76 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне CB");
                    gcodeBuilder.AppendLine("N77 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (3/8)");
                    gcodeBuilder.AppendLine("N78 M10()");
                    gcodeBuilder.AppendLine($"N79 G1 G42 X-{Math.Round((x / 8), 2)} Y0");
                    gcodeBuilder.AppendLine("N80 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне CB");
                    gcodeBuilder.AppendLine("N81 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (4/8)");
                    gcodeBuilder.AppendLine("N82 M10()");
                    gcodeBuilder.AppendLine($"N83 G1 G42 X-{Math.Round((x / 8), 2)} Y0");
                    gcodeBuilder.AppendLine("N84 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне CB");
                    gcodeBuilder.AppendLine("N85 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (5/8)");
                    gcodeBuilder.AppendLine("N86 M10()");
                    gcodeBuilder.AppendLine($"N87 G1 G42 X-{Math.Round((x / 8), 2)} Y0");                    
                    gcodeBuilder.AppendLine("N88 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне CB");
                    gcodeBuilder.AppendLine("N89 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (6/8)");
                    gcodeBuilder.AppendLine("N90 M10()");
                    gcodeBuilder.AppendLine($"N91 G1 G42 X-{Math.Round((x / 8), 2)} Y0");
                    gcodeBuilder.AppendLine("N92 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне CB");
                    gcodeBuilder.AppendLine("N93 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (7/8)");
                    gcodeBuilder.AppendLine("N94 M10()");
                    gcodeBuilder.AppendLine($"N95 G1 G42 X-{Math.Round((x / 8), 2)} Y0");
                    gcodeBuilder.AppendLine("N96 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне CB");
                    gcodeBuilder.AppendLine("N97 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (8/8)");
                    gcodeBuilder.AppendLine("N98 M10()");
                    gcodeBuilder.AppendLine($"N99 G1 G42 X-{Math.Round(((x / 8) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N100 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами CB и BA");
                    gcodeBuilder.AppendLine("N101 G0 G42 X-0.25 Y3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (1/8)");
                    gcodeBuilder.AppendLine("N102 M10()");
                    gcodeBuilder.AppendLine($"N103 G1 G42 X0 Y-{Math.Round(((y / 8) + 3), 2)}");
                    gcodeBuilder.AppendLine("N104 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне BA");
                    gcodeBuilder.AppendLine("N105 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (2/8)");
                    gcodeBuilder.AppendLine("N106 M10()");
                    gcodeBuilder.AppendLine($"N107 G1 G42 X0 Y-{Math.Round((y / 8), 2)}");
                    gcodeBuilder.AppendLine("N108 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне BA");
                    gcodeBuilder.AppendLine("N109 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (3/8)");
                    gcodeBuilder.AppendLine("N110 M10()");
                    gcodeBuilder.AppendLine($"N111 G1 G42 X0 Y-{Math.Round((y / 8), 2)}");
                    gcodeBuilder.AppendLine("N112 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне BA");
                    gcodeBuilder.AppendLine("N113 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (4/8)");
                    gcodeBuilder.AppendLine("N114 M10()");
                    gcodeBuilder.AppendLine($"N115 G1 G42 X0 Y-{Math.Round((y / 8), 2)}");
                    gcodeBuilder.AppendLine("N116 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне BA");
                    gcodeBuilder.AppendLine("N117 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (5/8)");
                    gcodeBuilder.AppendLine("N118 M10()");
                    gcodeBuilder.AppendLine($"N119 G1 G42 X0 Y-{Math.Round((y / 8), 2)}");
                    gcodeBuilder.AppendLine("N120 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне BA");
                    gcodeBuilder.AppendLine("N121 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (6/8)");
                    gcodeBuilder.AppendLine("N122 M10()");
                    gcodeBuilder.AppendLine($"N123 G1 G42 X0 Y-{Math.Round((y / 8), 2)}");
                    gcodeBuilder.AppendLine("N124 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне BA");
                    gcodeBuilder.AppendLine("N125 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (7/8)");
                    gcodeBuilder.AppendLine("N126 M10()");
                    gcodeBuilder.AppendLine($"N127 G1 G42 X0 Y-{Math.Round((y / 8), 2)}");
                    gcodeBuilder.AppendLine("N128 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне BA");
                    gcodeBuilder.AppendLine("N129 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (8/8)");
                    gcodeBuilder.AppendLine("N130 M10()");
                    gcodeBuilder.AppendLine($"N131 G1 G42 X0 Y-{Math.Round(((y / 8) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N132 M11()");

                    gcodeBuilder.AppendLine("; Конец программы");
                    gcodeBuilder.AppendLine("N133 M30");

                    GCodeTextBox.Text = gcodeBuilder.ToString().Replace(",", ".");
                    break;

                case 8:

                    gcodeBuilder.AppendLine($"; X = {x}, Y = {y}, кол-во перемычек на каждой стороне = 8, длина перемычек = 0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Начало программы");
                    gcodeBuilder.AppendLine("%");
                    gcodeBuilder.AppendLine("");

                    gcodeBuilder.AppendLine("; Переход к абсолютным координатам и перемещение на начало координат");
                    gcodeBuilder.AppendLine("N01 G90");
                    gcodeBuilder.AppendLine("N02 G54");
                    gcodeBuilder.AppendLine("N03 M13()");
                    gcodeBuilder.AppendLine("N04 G0 G42 X-1 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к относительным координатам и построение сторон детали");
                    gcodeBuilder.AppendLine("N05 G91");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (1/9)");
                    gcodeBuilder.AppendLine("N06 M10()");
                    gcodeBuilder.AppendLine($"N07 G1 G42 X{Math.Round((x / 9 + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N08 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне AD");
                    gcodeBuilder.AppendLine("N09 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (2/9)");
                    gcodeBuilder.AppendLine("N10 M10()");
                    gcodeBuilder.AppendLine($"N11 G1 G42 X{Math.Round((x / 9), 2)} Y0");
                    gcodeBuilder.AppendLine("N12 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне AD");
                    gcodeBuilder.AppendLine("N13 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (3/9)");
                    gcodeBuilder.AppendLine("N14 M10()");
                    gcodeBuilder.AppendLine($"N15 G1 G42 X{Math.Round((x / 9), 2)} Y0");
                    gcodeBuilder.AppendLine("N16 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне AD");
                    gcodeBuilder.AppendLine("N17 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (4/9)");
                    gcodeBuilder.AppendLine("N18 M10()");
                    gcodeBuilder.AppendLine($"N19 G1 G42 X{Math.Round((x / 9), 2)} Y0");
                    gcodeBuilder.AppendLine("N20 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне AD");
                    gcodeBuilder.AppendLine("N21 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (5/9)");
                    gcodeBuilder.AppendLine("N22 M10()");
                    gcodeBuilder.AppendLine($"N23 G1 G42 X{Math.Round((x / 9), 2)} Y0");
                    gcodeBuilder.AppendLine("N24 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне AD");
                    gcodeBuilder.AppendLine("N25 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (6/9)");
                    gcodeBuilder.AppendLine("N26 M10()");
                    gcodeBuilder.AppendLine($"N27 G1 G42 X{Math.Round((x / 9), 2)} Y0");
                    gcodeBuilder.AppendLine("N28 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне AD");
                    gcodeBuilder.AppendLine("N29 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (7/9)");
                    gcodeBuilder.AppendLine("N30 M10()");
                    gcodeBuilder.AppendLine($"N31 G1 G42 X{Math.Round((x / 9), 2)} Y0");
                    gcodeBuilder.AppendLine("N32 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне AD");
                    gcodeBuilder.AppendLine("N33 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (8/9)");
                    gcodeBuilder.AppendLine("N34 M10()");
                    gcodeBuilder.AppendLine($"N35 G1 G42 X{Math.Round((x / 9), 2)} Y0");
                    gcodeBuilder.AppendLine("N36 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №8 на стороне AD");
                    gcodeBuilder.AppendLine("N37 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (9/9)");
                    gcodeBuilder.AppendLine("N38 M10()");
                    gcodeBuilder.AppendLine($"N39 G1 G42 X{Math.Round(((x / 9) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N40 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами AD и DC");
                    gcodeBuilder.AppendLine("N41 G0 G42 X0.25 Y-3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (1/9)");
                    gcodeBuilder.AppendLine("N42 M10()");
                    gcodeBuilder.AppendLine($"N43 G1 G42 X0 Y{Math.Round(((y / 9) + 3), 2)}");
                    gcodeBuilder.AppendLine("N44 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне DC");
                    gcodeBuilder.AppendLine("N45 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (2/9)");
                    gcodeBuilder.AppendLine("N46 M10()");
                    gcodeBuilder.AppendLine($"N47 G1 G42 X0 Y{Math.Round((y / 9), 2)}");
                    gcodeBuilder.AppendLine("N48 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне DC");
                    gcodeBuilder.AppendLine("N49 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (3/9)");
                    gcodeBuilder.AppendLine("N50 M10()");
                    gcodeBuilder.AppendLine($"N51 G1 G42 X0 Y{Math.Round((y / 9), 2)}");
                    gcodeBuilder.AppendLine("N52 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне DC");
                    gcodeBuilder.AppendLine("N53 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (4/9)");
                    gcodeBuilder.AppendLine("N54 M10()");
                    gcodeBuilder.AppendLine($"N55 G1 G42 X0 Y{Math.Round((y / 9), 2)}");
                    gcodeBuilder.AppendLine("N56 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне DC");
                    gcodeBuilder.AppendLine("N57 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (5/9)");
                    gcodeBuilder.AppendLine("N58 M10()");
                    gcodeBuilder.AppendLine($"N59 G1 G42 X0 Y{Math.Round((y / 9), 2)}");
                    gcodeBuilder.AppendLine("N60 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне DC");
                    gcodeBuilder.AppendLine("N61 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (6/9)");
                    gcodeBuilder.AppendLine("N62 M10()");
                    gcodeBuilder.AppendLine($"N63 G1 G42 X0 Y{Math.Round((y / 9), 2)}");
                    gcodeBuilder.AppendLine("N64 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне DC");
                    gcodeBuilder.AppendLine("N65 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (7/9)");
                    gcodeBuilder.AppendLine("N66 M10()");
                    gcodeBuilder.AppendLine($"N67 G1 G42 X0 Y{Math.Round((y / 9), 2)}");
                    gcodeBuilder.AppendLine("N68 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне DC");
                    gcodeBuilder.AppendLine("N69 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (8/9)");
                    gcodeBuilder.AppendLine("N70 M10()");
                    gcodeBuilder.AppendLine($"N71 G1 G42 X0 Y{Math.Round((y / 9), 2)}");
                    gcodeBuilder.AppendLine("N72 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №8 на стороне DC");
                    gcodeBuilder.AppendLine("N73 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (9/9)");
                    gcodeBuilder.AppendLine("N74 M10()");
                    gcodeBuilder.AppendLine($"N75 G1 G42 X0 Y{Math.Round(((y / 9) - 0.55 * selectedNumber), 2)}"); 
                    gcodeBuilder.AppendLine("N76 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами DC и CB");
                    gcodeBuilder.AppendLine("N77 G0 G42 X3 Y0.25");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (1/9)");
                    gcodeBuilder.AppendLine("N78 M10()");
                    gcodeBuilder.AppendLine($"N79 G1 G42 X-{Math.Round(((x / 9) + 5), 2)} Y0");
                    gcodeBuilder.AppendLine("N80 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне CB");
                    gcodeBuilder.AppendLine("N81 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (2/9)");
                    gcodeBuilder.AppendLine("N82 M10()");
                    gcodeBuilder.AppendLine($"N83 G1 G42 X-{Math.Round((x / 9), 2)} Y0");
                    gcodeBuilder.AppendLine("N84 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне CB");
                    gcodeBuilder.AppendLine("N85 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (3/9)");
                    gcodeBuilder.AppendLine("N86 M10()");
                    gcodeBuilder.AppendLine($"N87 G1 G42 X-{Math.Round((x / 9), 2)} Y0");
                    gcodeBuilder.AppendLine("N88 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне CB");
                    gcodeBuilder.AppendLine("N89 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (4/9)");
                    gcodeBuilder.AppendLine("N90 M10()");
                    gcodeBuilder.AppendLine($"N91 G1 G42 X-{Math.Round((x / 9), 2)} Y0");
                    gcodeBuilder.AppendLine("N92 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне CB");
                    gcodeBuilder.AppendLine("N93 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (5/9)");
                    gcodeBuilder.AppendLine("N94 M10()");
                    gcodeBuilder.AppendLine($"N95 G1 G42 X-{Math.Round((x / 9), 2)} Y0");
                    gcodeBuilder.AppendLine("N96 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне CB");
                    gcodeBuilder.AppendLine("N97 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (6/9)");
                    gcodeBuilder.AppendLine("N98 M10()");
                    gcodeBuilder.AppendLine($"N99 G1 G42 X-{Math.Round((x / 9), 2)} Y0");
                    gcodeBuilder.AppendLine("N100 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне CB");
                    gcodeBuilder.AppendLine("N101 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (7/9)");
                    gcodeBuilder.AppendLine("N102 M10()");
                    gcodeBuilder.AppendLine($"N103 G1 G42 X-{Math.Round((x / 9), 2)} Y0");
                    gcodeBuilder.AppendLine("N104 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне CB");
                    gcodeBuilder.AppendLine("N105 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (8/9)");
                    gcodeBuilder.AppendLine("N106 M10()");
                    gcodeBuilder.AppendLine($"N107 G1 G42 X-{Math.Round((x / 9), 2)} Y0");
                    gcodeBuilder.AppendLine("N108 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №8 на стороне CB");
                    gcodeBuilder.AppendLine("N109 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (9/9)");
                    gcodeBuilder.AppendLine("N110 M10()");
                    gcodeBuilder.AppendLine($"N111 G1 G42 X-{Math.Round(((x / 9) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N112 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами CB и BA");
                    gcodeBuilder.AppendLine("N113 G0 G42 X-0.25 Y3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (1/9)");
                    gcodeBuilder.AppendLine("N114 M10()");
                    gcodeBuilder.AppendLine($"N115 G1 G42 X0 Y-{Math.Round(((y / 9) + 3), 2)}");
                    gcodeBuilder.AppendLine("N116 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне BA");
                    gcodeBuilder.AppendLine("N117 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (2/9)");
                    gcodeBuilder.AppendLine("N118 M10()");
                    gcodeBuilder.AppendLine($"N119 G1 G42 X0 Y-{Math.Round((y / 9), 2)}");
                    gcodeBuilder.AppendLine("N120 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне BA");
                    gcodeBuilder.AppendLine("N121 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (3/9)");
                    gcodeBuilder.AppendLine("N122 M10()");
                    gcodeBuilder.AppendLine($"N123 G1 G42 X0 Y-{Math.Round((y / 9), 2)}");
                    gcodeBuilder.AppendLine("N124 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне BA");
                    gcodeBuilder.AppendLine("N125 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (4/9)");
                    gcodeBuilder.AppendLine("N126 M10()");
                    gcodeBuilder.AppendLine($"N127 G1 G42 X0 Y-{Math.Round((y / 9), 2)}");
                    gcodeBuilder.AppendLine("N128 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне BA");
                    gcodeBuilder.AppendLine("N129 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (5/9)");
                    gcodeBuilder.AppendLine("N130 M10()");
                    gcodeBuilder.AppendLine($"N131 G1 G42 X0 Y-{Math.Round((y / 9), 2)}");
                    gcodeBuilder.AppendLine("N132 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне BA");
                    gcodeBuilder.AppendLine("N133 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (6/9)");
                    gcodeBuilder.AppendLine("N134 M10()");
                    gcodeBuilder.AppendLine($"N135 G1 G42 X0 Y-{Math.Round((y / 9), 2)}");
                    gcodeBuilder.AppendLine("N136 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне BA");
                    gcodeBuilder.AppendLine("N137 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (7/9)");
                    gcodeBuilder.AppendLine("N138 M10()");
                    gcodeBuilder.AppendLine($"N139 G1 G42 X0 Y-{Math.Round((y / 9), 2)}");
                    gcodeBuilder.AppendLine("N140 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне BA");
                    gcodeBuilder.AppendLine("N141 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (8/9)");
                    gcodeBuilder.AppendLine("N142 M10()");
                    gcodeBuilder.AppendLine($"N143 G1 G42 X0 Y-{Math.Round((y / 9), 2)}");
                    gcodeBuilder.AppendLine("N144 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №8 на стороне BA");
                    gcodeBuilder.AppendLine("N145 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (9/9)");
                    gcodeBuilder.AppendLine("N146 M10()");
                    gcodeBuilder.AppendLine($"N147 G1 G42 X0 Y-{Math.Round(((y / 9) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N148 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Конец программы");
                    gcodeBuilder.AppendLine("N149 M30");

                    GCodeTextBox.Text = gcodeBuilder.ToString().Replace(",", ".");
                    break;

                case 9:

                    gcodeBuilder.AppendLine($"; X = {x}, Y = {y}, кол-во перемычек на каждой стороне = 9, длина перемычек = 0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Начало программы");
                    gcodeBuilder.AppendLine("%");
                    gcodeBuilder.AppendLine("");

                    gcodeBuilder.AppendLine("; Переход к абсолютным координатам и перемещение на начало координат");
                    gcodeBuilder.AppendLine("N01 G90");
                    gcodeBuilder.AppendLine("N02 G54");
                    gcodeBuilder.AppendLine("N03 M13()");
                    gcodeBuilder.AppendLine("N04 G0 G42 X-1 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к относительным координатам и построение сторон детали");
                    gcodeBuilder.AppendLine("N05 G91");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (1/10)");
                    gcodeBuilder.AppendLine("N06 M10()");
                    gcodeBuilder.AppendLine($"N07 G1 G42 X{Math.Round((x / 10 + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N08 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне AD");
                    gcodeBuilder.AppendLine("N09 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (2/10)");
                    gcodeBuilder.AppendLine("N10 M10()");
                    gcodeBuilder.AppendLine($"N11 G1 G42 X{Math.Round((x / 10), 2)} Y0");
                    gcodeBuilder.AppendLine("N12 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне AD");
                    gcodeBuilder.AppendLine("N13 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (3/10)");
                    gcodeBuilder.AppendLine("N14 M10()");
                    gcodeBuilder.AppendLine($"N15 G1 G42 X{Math.Round((x / 10), 2)} Y0");
                    gcodeBuilder.AppendLine("N16 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне AD");
                    gcodeBuilder.AppendLine("N17 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (4/10)");
                    gcodeBuilder.AppendLine("N18 M10()");
                    gcodeBuilder.AppendLine($"N19 G1 G42 X{Math.Round((x / 10), 2)} Y0");
                    gcodeBuilder.AppendLine("N20 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне AD");
                    gcodeBuilder.AppendLine("N21 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (5/10)");
                    gcodeBuilder.AppendLine("N22 M10()");
                    gcodeBuilder.AppendLine($"N23 G1 G42 X{Math.Round((x / 10), 2)} Y0");
                    gcodeBuilder.AppendLine("N24 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне AD");
                    gcodeBuilder.AppendLine("N25 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (6/10)");
                    gcodeBuilder.AppendLine("N26 M10()");
                    gcodeBuilder.AppendLine($"N27 G1 G42 X{Math.Round((x / 10), 2)} Y0");
                    gcodeBuilder.AppendLine("N28 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне AD");
                    gcodeBuilder.AppendLine("N29 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (7/10)");
                    gcodeBuilder.AppendLine("N30 M10()");
                    gcodeBuilder.AppendLine($"N31 G1 G42 X{Math.Round((x / 10), 2)} Y0");
                    gcodeBuilder.AppendLine("N32 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне AD");
                    gcodeBuilder.AppendLine("N33 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (8/10)");
                    gcodeBuilder.AppendLine("N34 M10()");
                    gcodeBuilder.AppendLine($"N35 G1 G42 X{Math.Round((x / 10), 2)} Y0");
                    gcodeBuilder.AppendLine("N36 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №8 на стороне AD");
                    gcodeBuilder.AppendLine("N37 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (9/10)");
                    gcodeBuilder.AppendLine("N38 M10()");
                    gcodeBuilder.AppendLine($"N39 G1 G42 X{Math.Round((x / 10), 2)} Y0");
                    gcodeBuilder.AppendLine("N40 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №9 на стороне AD");
                    gcodeBuilder.AppendLine("N41 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (10/10)");
                    gcodeBuilder.AppendLine("N42 M10()");
                    gcodeBuilder.AppendLine($"N43 G1 G42 X{Math.Round(((x / 10) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N44 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами AD и DC");
                    gcodeBuilder.AppendLine("N45 G0 G42 X0.25 Y-3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (1/10)");
                    gcodeBuilder.AppendLine("N46 M10()");
                    gcodeBuilder.AppendLine($"N47 G1 G42 X0 Y{Math.Round(((y / 10) + 3), 2)}");
                    gcodeBuilder.AppendLine("N48 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне DC");
                    gcodeBuilder.AppendLine("N49 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (2/10)");
                    gcodeBuilder.AppendLine("N50 M10()");
                    gcodeBuilder.AppendLine($"N51 G1 G42 X0 Y{Math.Round((y / 10), 2)}");
                    gcodeBuilder.AppendLine("N52 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне DC");
                    gcodeBuilder.AppendLine("N53 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (3/10)");
                    gcodeBuilder.AppendLine("N54 M10()");
                    gcodeBuilder.AppendLine($"N55 G1 G42 X0 Y{Math.Round((y / 10), 2)}");
                    gcodeBuilder.AppendLine("N56 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне DC");
                    gcodeBuilder.AppendLine("N57 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (4/10)");
                    gcodeBuilder.AppendLine("N58 M10()");
                    gcodeBuilder.AppendLine($"N59 G1 G42 X0 Y{Math.Round((y / 10), 2)}");
                    gcodeBuilder.AppendLine("N60 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне DC");
                    gcodeBuilder.AppendLine("N61 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (5/10)");
                    gcodeBuilder.AppendLine("N62 M10()");
                    gcodeBuilder.AppendLine($"N63 G1 G42 X0 Y{Math.Round((y / 10), 2)}");
                    gcodeBuilder.AppendLine("N64 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне DC");
                    gcodeBuilder.AppendLine("N65 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (6/10)");
                    gcodeBuilder.AppendLine("N66 M10()");
                    gcodeBuilder.AppendLine($"N67 G1 G42 X0 Y{Math.Round((y / 10), 2)}");
                    gcodeBuilder.AppendLine("N68 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне DC");
                    gcodeBuilder.AppendLine("N69 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (7/10)");
                    gcodeBuilder.AppendLine("N70 M10()");
                    gcodeBuilder.AppendLine($"N71 G1 G42 X0 Y{Math.Round((y / 10), 2)}");
                    gcodeBuilder.AppendLine("N72 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне DC");
                    gcodeBuilder.AppendLine("N73 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (8/10)");
                    gcodeBuilder.AppendLine("N74 M10()");
                    gcodeBuilder.AppendLine($"N75 G1 G42 X0 Y{Math.Round((y / 10), 2)}");
                    gcodeBuilder.AppendLine("N76 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №8 на стороне DC");
                    gcodeBuilder.AppendLine("N77 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (9/10)");
                    gcodeBuilder.AppendLine("N78 M10()");
                    gcodeBuilder.AppendLine($"N79 G1 G42 X0 Y{Math.Round((y / 10), 2)}");
                    gcodeBuilder.AppendLine("N80 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №9 на стороне DC");
                    gcodeBuilder.AppendLine("N81 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (10/10)");
                    gcodeBuilder.AppendLine("N82 M10()");
                    gcodeBuilder.AppendLine($"N83 G1 G42 X0 Y{Math.Round(((y / 10) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N84 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами DC и CB");
                    gcodeBuilder.AppendLine("N85 G0 G42 X3 Y0.25");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (1/10)");
                    gcodeBuilder.AppendLine("N86 M10()");
                    gcodeBuilder.AppendLine($"N87 G1 G42 X-{Math.Round(((x / 10) + 5), 2)} Y0");
                    gcodeBuilder.AppendLine("N88 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне CB");
                    gcodeBuilder.AppendLine("N89 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (2/10)");
                    gcodeBuilder.AppendLine("N90 M10()");
                    gcodeBuilder.AppendLine($"N91 G1 G42 X-{Math.Round((x / 10), 2)} Y0");
                    gcodeBuilder.AppendLine("N92 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне CB");
                    gcodeBuilder.AppendLine("N93 G0 G42 X-0.3 Y0"); 

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (3/10)");
                    gcodeBuilder.AppendLine("N94 M10()");
                    gcodeBuilder.AppendLine($"N95 G1 G42 X-{Math.Round((x / 10), 2)} Y0");
                    gcodeBuilder.AppendLine("N96 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне CB");
                    gcodeBuilder.AppendLine("N97 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (4/10)");
                    gcodeBuilder.AppendLine("N98 M10()");
                    gcodeBuilder.AppendLine($"N99 G1 G42 X-{Math.Round((x / 10), 2)} Y0");
                    gcodeBuilder.AppendLine("N100 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне CB");
                    gcodeBuilder.AppendLine("N101 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (5/10)");
                    gcodeBuilder.AppendLine("N102 M10()");
                    gcodeBuilder.AppendLine($"N103 G1 G42 X-{Math.Round((x / 10), 2)} Y0"); 
                    gcodeBuilder.AppendLine("N104 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне CB");
                    gcodeBuilder.AppendLine("N105 G0 G42 X-0.3 Y0"); 

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (6/10)");
                    gcodeBuilder.AppendLine("N106 M10()");
                    gcodeBuilder.AppendLine($"N107 G1 G42 X-{Math.Round((x / 10), 2)} Y0");
                    gcodeBuilder.AppendLine("N108 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне CB");
                    gcodeBuilder.AppendLine("N109 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (7/10)");
                    gcodeBuilder.AppendLine("N110 M10()");
                    gcodeBuilder.AppendLine($"N111 G1 G42 X-{Math.Round((x / 10), 2)} Y0");
                    gcodeBuilder.AppendLine("N112 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне CB");
                    gcodeBuilder.AppendLine("N113 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (8/10)");
                    gcodeBuilder.AppendLine("N114 M10()");
                    gcodeBuilder.AppendLine($"N115 G1 G42 X-{Math.Round((x / 10), 2)} Y0");
                    gcodeBuilder.AppendLine("N116 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №8 на стороне CB");
                    gcodeBuilder.AppendLine("N117 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (9/10)");
                    gcodeBuilder.AppendLine("N118 M10()");
                    gcodeBuilder.AppendLine($"N119 G1 G42 X-{Math.Round((x / 10), 2)} Y0"); 
                    gcodeBuilder.AppendLine("N120 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №9 на стороне CB");
                    gcodeBuilder.AppendLine("N121 G0 G42 X-0.3 Y0"); 

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (10/10)");
                    gcodeBuilder.AppendLine("N122 M10()");
                    gcodeBuilder.AppendLine($"N123 G1 G42 X-{Math.Round(((x / 10) - 0.55 * selectedNumber), 2)} Y0"); 
                    gcodeBuilder.AppendLine("N124 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами CB и BA");
                    gcodeBuilder.AppendLine("N125 G0 G42 X-0.25 Y3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (1/10)");
                    gcodeBuilder.AppendLine("N126 M10()");
                    gcodeBuilder.AppendLine($"N127 G1 G42 X0 Y-{Math.Round(((y / 10) + 3), 2)}"); 
                    gcodeBuilder.AppendLine("N128 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне BA");
                    gcodeBuilder.AppendLine("N129 G0 G42 X0 Y-0.3"); 

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (2/10)");
                    gcodeBuilder.AppendLine("N130 M10()");
                    gcodeBuilder.AppendLine($"N131 G1 G42 X0 Y-{Math.Round((y / 10), 2)}");
                    gcodeBuilder.AppendLine("N132 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне BA");
                    gcodeBuilder.AppendLine("N133 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (3/10)");
                    gcodeBuilder.AppendLine("N134 M10()");
                    gcodeBuilder.AppendLine($"N135 G1 G42 X0 Y-{Math.Round((y / 10), 2)}");
                    gcodeBuilder.AppendLine("N136 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне BA");
                    gcodeBuilder.AppendLine("N137 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (4/10)");
                    gcodeBuilder.AppendLine("N138 M10()");
                    gcodeBuilder.AppendLine($"N139 G1 G42 X0 Y-{Math.Round((y / 10), 2)}");
                    gcodeBuilder.AppendLine("N140 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне BA");
                    gcodeBuilder.AppendLine("N141 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (5/10)");
                    gcodeBuilder.AppendLine("N142 M10()");
                    gcodeBuilder.AppendLine($"N143 G1 G42 X0 Y-{Math.Round((y / 10), 2)}");
                    gcodeBuilder.AppendLine("N144 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне BA");
                    gcodeBuilder.AppendLine("N145 G0 G42 X0 Y-0.3"); 

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (6/10)");
                    gcodeBuilder.AppendLine("N146 M10()");
                    gcodeBuilder.AppendLine($"N147 G1 G42 X0 Y-{Math.Round((y / 10), 2)}");
                    gcodeBuilder.AppendLine("N148 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне BA");
                    gcodeBuilder.AppendLine("N149 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (7/10)");
                    gcodeBuilder.AppendLine("N150 M10()");
                    gcodeBuilder.AppendLine($"N151 G1 G42 X0 Y-{Math.Round((y / 10), 2)}"); 
                    gcodeBuilder.AppendLine("N152 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне BA");
                    gcodeBuilder.AppendLine("N153 G0 G42 X0 Y-0.3"); 

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (8/10)");
                    gcodeBuilder.AppendLine("N154 M10()");
                    gcodeBuilder.AppendLine($"N155 G1 G42 X0 Y-{Math.Round((y / 10), 2)}");
                    gcodeBuilder.AppendLine("N156 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №8 на стороне BA");
                    gcodeBuilder.AppendLine("N157 G0 G42 X0 Y-0.3"); 

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (9/10)");
                    gcodeBuilder.AppendLine("N158 M10()");
                    gcodeBuilder.AppendLine($"N159 G1 G42 X0 Y-{Math.Round((y / 10), 2)}"); 
                    gcodeBuilder.AppendLine("N160 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №9 на стороне BA");
                    gcodeBuilder.AppendLine("N161 G0 G42 X0 Y-0.3"); 

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (10/10)");
                    gcodeBuilder.AppendLine("N162 M10()");
                    gcodeBuilder.AppendLine($"N163 G1 G42 X0 Y-{Math.Round(((y / 10) - 0.55 * selectedNumber), 2)}"); 
                    gcodeBuilder.AppendLine("N164 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Конец программы");
                    gcodeBuilder.AppendLine("N165 M30");

                    GCodeTextBox.Text = gcodeBuilder.ToString().Replace(",", ".");
                    break;

                case 10:

                    gcodeBuilder.AppendLine($"; X = {x}, Y = {y}, кол-во перемычек на каждой стороне = 10, длина перемычек = 0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Начало программы");
                    gcodeBuilder.AppendLine("%");

                    gcodeBuilder.AppendLine("; Переход к абсолютным координатам и перемещение на начало координат");
                    gcodeBuilder.AppendLine("N01 G90");
                    gcodeBuilder.AppendLine("N02 G54");
                    gcodeBuilder.AppendLine("N03 M13()");
                    gcodeBuilder.AppendLine("N04 G0 G42 X-1 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Переход к относительным координатам и построение сторон детали");
                    gcodeBuilder.AppendLine("N05 G91");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (1/11)");
                    gcodeBuilder.AppendLine("N06 M10()");
                    gcodeBuilder.AppendLine($"N07 G1 G42 X{Math.Round((x / 11 + 3), 2)} Y0");
                    gcodeBuilder.AppendLine("N08 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне AD");
                    gcodeBuilder.AppendLine("N09 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (2/11)");
                    gcodeBuilder.AppendLine("N10 M10()");
                    gcodeBuilder.AppendLine($"N11 G1 G42 X{Math.Round((x / 11),2)} Y0");
                    gcodeBuilder.AppendLine("N12 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне AD");
                    gcodeBuilder.AppendLine("N13 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (3/11)");
                    gcodeBuilder.AppendLine("N14 M10()");
                    gcodeBuilder.AppendLine($"N15 G1 G42 X{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N16 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне AD");
                    gcodeBuilder.AppendLine("N17 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (4/11)");
                    gcodeBuilder.AppendLine("N18 M10()");
                    gcodeBuilder.AppendLine($"N19 G1 G42 X{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N20 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне AD");
                    gcodeBuilder.AppendLine("N21 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (5/11)");
                    gcodeBuilder.AppendLine("N22 M10()");
                    gcodeBuilder.AppendLine($"N23 G1 G42 X{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N24 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне AD");
                    gcodeBuilder.AppendLine("N25 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (6/11)");
                    gcodeBuilder.AppendLine("N26 M10()");
                    gcodeBuilder.AppendLine($"N27 G1 G42 X{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N28 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне AD");
                    gcodeBuilder.AppendLine("N29 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (7/11)");
                    gcodeBuilder.AppendLine("N30 M10()");
                    gcodeBuilder.AppendLine($"N31 G1 G42 X{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N32 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне AD");
                    gcodeBuilder.AppendLine("N33 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (8/11)");
                    gcodeBuilder.AppendLine("N34 M10()");
                    gcodeBuilder.AppendLine($"N35 G1 G42 X{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N36 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №8 на стороне AD");
                    gcodeBuilder.AppendLine("N37 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (9/11)");
                    gcodeBuilder.AppendLine("N38 M10()");
                    gcodeBuilder.AppendLine($"N39 G1 G42 X{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N40 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №9 на стороне AD");
                    gcodeBuilder.AppendLine("N41 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (10/11)");
                    gcodeBuilder.AppendLine("N42 M10()");
                    gcodeBuilder.AppendLine($"N43 G1 G42 X{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N44 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №10 на стороне AD");
                    gcodeBuilder.AppendLine("N45 G0 G42 X0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона AD (11/11)");
                    gcodeBuilder.AppendLine("N46 M10()");
                    gcodeBuilder.AppendLine($"N47 G1 G42 X{Math.Round(((x / 11) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N48 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами AD и DC");
                    gcodeBuilder.AppendLine("N49 G0 G42 X0.25 Y-3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (1/11)");
                    gcodeBuilder.AppendLine("N50 M10()");
                    gcodeBuilder.AppendLine($"N51 G1 G42 X0 Y{Math.Round(((y / 11) + 3), 2)}");
                    gcodeBuilder.AppendLine("N52 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне DC");
                    gcodeBuilder.AppendLine("N53 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (2/11)");
                    gcodeBuilder.AppendLine("N54 M10()");
                    gcodeBuilder.AppendLine($"N55 G1 G42 X0 Y{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N56 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне DC");
                    gcodeBuilder.AppendLine("N57 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (3/11)");
                    gcodeBuilder.AppendLine("N58 M10()");
                    gcodeBuilder.AppendLine($"N59 G1 G42 X0 Y{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N60 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне DC");
                    gcodeBuilder.AppendLine("N61 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (4/11)");
                    gcodeBuilder.AppendLine("N62 M10()");
                    gcodeBuilder.AppendLine($"N63 G1 G42 X0 Y{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N64 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне DC");
                    gcodeBuilder.AppendLine("N65 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (5/11)");
                    gcodeBuilder.AppendLine("N66 M10()");
                    gcodeBuilder.AppendLine($"N67 G1 G42 X0 Y{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N68 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне DC");
                    gcodeBuilder.AppendLine("N69 G0 G42 X0 Y0.3");
                    
                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (6/11)");
                    gcodeBuilder.AppendLine("N70 M10()");
                    gcodeBuilder.AppendLine($"N71 G1 G42 X0 Y{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N72 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне DC");
                    gcodeBuilder.AppendLine("N73 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (7/11)");
                    gcodeBuilder.AppendLine("N74 M10()");
                    gcodeBuilder.AppendLine($"N75 G1 G42 X0 Y{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N76 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне DC");
                    gcodeBuilder.AppendLine("N77 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (8/11)");
                    gcodeBuilder.AppendLine("N78 M10()");
                    gcodeBuilder.AppendLine($"N79 G1 G42 X0 Y{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N80 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №8 на стороне DC");
                    gcodeBuilder.AppendLine("N81 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (9/11)");
                    gcodeBuilder.AppendLine("N82 M10()");
                    gcodeBuilder.AppendLine($"N83 G1 G42 X0 Y{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N84 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №9 на стороне DC");
                    gcodeBuilder.AppendLine("N85 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (10/11)");
                    gcodeBuilder.AppendLine("N86 M10()");
                    gcodeBuilder.AppendLine($"N87 G1 G42 X0 Y{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N88 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №10 на стороне DC");
                    gcodeBuilder.AppendLine("N89 G0 G42 X0 Y0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона DC (11/11)");
                    gcodeBuilder.AppendLine("N90 M10()");
                    gcodeBuilder.AppendLine($"N91 G1 G42 X0 Y{Math.Round(((y / 11) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N92 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами DC и CB");
                    gcodeBuilder.AppendLine("N93 G0 G42 X3 Y0.25");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (1/11)");
                    gcodeBuilder.AppendLine("N94 M10()");
                    gcodeBuilder.AppendLine($"N95 G1 G42 X-{Math.Round(((x / 11) + 5), 2)} Y0");
                    gcodeBuilder.AppendLine("N96 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне CB");
                    gcodeBuilder.AppendLine("N97 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (2/11)");
                    gcodeBuilder.AppendLine("N98 M10()");
                    gcodeBuilder.AppendLine($"N99 G1 G42 X-{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N100 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне CB");
                    gcodeBuilder.AppendLine("N101 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (3/11)");
                    gcodeBuilder.AppendLine("N102 M10()");
                    gcodeBuilder.AppendLine($"N103 G1 G42 X-{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N104 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне CB");
                    gcodeBuilder.AppendLine("N105 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (4/11)");
                    gcodeBuilder.AppendLine("N106 M10()");
                    gcodeBuilder.AppendLine($"N107 G1 G42 X-{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N108 M11()");
                    
                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне CB");
                    gcodeBuilder.AppendLine("N109 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (5/11)");
                    gcodeBuilder.AppendLine("N110 M10()");
                    gcodeBuilder.AppendLine($"N111 G1 G42 X-{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N112 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне CB");
                    gcodeBuilder.AppendLine("N113 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (6/11)");
                    gcodeBuilder.AppendLine("N114 M10()");
                    gcodeBuilder.AppendLine($"N115 G1 G42 X-{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N116 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне CB");
                    gcodeBuilder.AppendLine("N117 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (7/11)");
                    gcodeBuilder.AppendLine("N118 M10()");
                    gcodeBuilder.AppendLine($"N119 G1 G42 X-{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N120 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне CB");
                    gcodeBuilder.AppendLine("N121 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (8/11)");
                    gcodeBuilder.AppendLine("N122 M10()");
                    gcodeBuilder.AppendLine($"N123 G1 G42 X-{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N124 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №8 на стороне CB");
                    gcodeBuilder.AppendLine("N125 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (9/11)");
                    gcodeBuilder.AppendLine("N126 M10()");
                    gcodeBuilder.AppendLine($"N127 G1 G42 X-{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N128 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №9 на стороне CB");
                    gcodeBuilder.AppendLine("N129 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (10/11)");
                    gcodeBuilder.AppendLine("N130 M10()");
                    gcodeBuilder.AppendLine($"N131 G1 G42 X-{Math.Round((x / 11), 2)} Y0");
                    gcodeBuilder.AppendLine("N132 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №10 на стороне CB");
                    gcodeBuilder.AppendLine("N133 G0 G42 X-0.3 Y0");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона CB (11/11)");
                    gcodeBuilder.AppendLine("N134 M10()");
                    gcodeBuilder.AppendLine($"N135 G1 G42 X-{Math.Round(((x / 11) - 0.55 * selectedNumber), 2)} Y0");
                    gcodeBuilder.AppendLine("N136 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Угловая врезка между сторонами CB и BA");
                    gcodeBuilder.AppendLine("N137 G0 G42 X-0.25 Y3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (1/11)");
                    gcodeBuilder.AppendLine("N138 M10()");
                    gcodeBuilder.AppendLine($"N139 G1 G42 X0 Y-{Math.Round(((y / 11) + 3), 2)}");
                    gcodeBuilder.AppendLine("N140 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №1 на стороне BA");
                    gcodeBuilder.AppendLine("N141 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (2/11)");
                    gcodeBuilder.AppendLine("N142 M10()");
                    gcodeBuilder.AppendLine($"N143 G1 G42 X0 Y-{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N144 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №2 на стороне BA");
                    gcodeBuilder.AppendLine("N145 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (3/11)");
                    gcodeBuilder.AppendLine("N146 M10()");
                    gcodeBuilder.AppendLine($"N147 G1 G42 X0 Y-{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N148 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №3 на стороне BA");
                    gcodeBuilder.AppendLine("N149 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (4/11)");
                    gcodeBuilder.AppendLine("N150 M10()");
                    gcodeBuilder.AppendLine($"N151 G1 G42 X0 Y-{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N152 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №4 на стороне BA");
                    gcodeBuilder.AppendLine("N153 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (5/11)");
                    gcodeBuilder.AppendLine("N154 M10()");
                    gcodeBuilder.AppendLine($"N155 G1 G42 X0 Y-{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N156 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №5 на стороне BA");
                    gcodeBuilder.AppendLine("N157 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (6/11)");
                    gcodeBuilder.AppendLine("N158 M10()");
                    gcodeBuilder.AppendLine($"N159 G1 G42 X0 Y-{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N160 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №6 на стороне BA");
                    gcodeBuilder.AppendLine("N161 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (7/11)");
                    gcodeBuilder.AppendLine("N162 M10()");
                    gcodeBuilder.AppendLine($"N163 G1 G42 X0 Y-{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N164 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №7 на стороне BA");
                    gcodeBuilder.AppendLine("N165 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (8/11)");
                    gcodeBuilder.AppendLine("N166 M10()");
                    gcodeBuilder.AppendLine($"N167 G1 G42 X0 Y-{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N168 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №8 на стороне BA");
                    gcodeBuilder.AppendLine("N169 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (9/11)");
                    gcodeBuilder.AppendLine("N170 M10()");
                    gcodeBuilder.AppendLine($"N171 G1 G42 X0 Y-{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N172 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №9 на стороне BA");
                    gcodeBuilder.AppendLine("N173 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (10/11)");
                    gcodeBuilder.AppendLine("N174 M10()");
                    gcodeBuilder.AppendLine($"N175 G1 G42 X0 Y-{Math.Round((y / 11), 2)}");
                    gcodeBuilder.AppendLine("N176 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Перемычка №10 на стороне BA");
                    gcodeBuilder.AppendLine("N177 G0 G42 X0 Y-0.3");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Сторона BA (11/11)");
                    gcodeBuilder.AppendLine("N178 M10()");
                    gcodeBuilder.AppendLine($"N179 G1 G42 X0 Y-{Math.Round(((y / 11) - 0.55 * selectedNumber), 2)}");
                    gcodeBuilder.AppendLine("N180 M11()");

                    gcodeBuilder.AppendLine("");
                    gcodeBuilder.AppendLine("; Конец программы");
                    gcodeBuilder.AppendLine("N181 M30");

                    GCodeTextBox.Text = gcodeBuilder.ToString().Replace(",", ".");
                    break;

            }

            MessageBox.Show("Код сгенерирован", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, есть ли сгенерированный код
            if (string.IsNullOrWhiteSpace(GCodeTextBox.Text))
            {
                MessageBox.Show("Сначала сгенерируйте G-код перед сохранением файла.");
                return;
            }

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = $"Прямоугольник({XTextBox.Text}x{YTextBox.Text}, кол-во перемычек = {NumberComboBox.SelectedIndex})"; // Имя файла по умолчанию
            dlg.DefaultExt = ".iso"; // Расширение по умолчанию
            dlg.Filter = "ISO documents (.iso)|*.iso|Text documents (.txt)|*.txt|CNC files (.cnc)|*.cnc|GCode files (.gcode)|*.gcode"; // Фильтры файлов

            // Показываем диалог сохранения файла
            Nullable<bool> result = dlg.ShowDialog();

            // Если пользователь нажал "Сохранить"
            if (result == true)
            {
                // Получаем путь к файлу
                string filename = dlg.FileName;

                // Сохраняем содержимое GCodeTextBox в выбранный файл
                try
                {
                    System.IO.File.WriteAllText(filename, GCodeTextBox.Text, Encoding.UTF8);
                    MessageBox.Show("Файл успешно сохранен.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении файла: " + ex.Message);
                }
            }
        }
    }
}

