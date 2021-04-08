using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tamagotchi
{
    class RankingFactory
    {
        public static void PopulateRanking(Panel panel, List<PlayerData> data)
        {
            panel.Children.Clear();
            data.Sort((a,b) => b.BestScore.Seconds - a.BestScore.Seconds);
            for (int i = 0; i < data.Count; i++)
                panel.Children.Add(GetEntry(data[i], string.Format("{0}. ", i+1)));
        }

        public static Grid GetEntry(PlayerData data, string prefix="")
        {
            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            Label title = new Label();
            title.Content = prefix + data.Name;
            title.Foreground = Brushes.White;
            title.FontSize = 15;
            Grid.SetRow(title, 0);
            Grid.SetColumn(title, 0);

            Label score = new Label();
            score.Content = data.BestScore.ToString();
            score.Foreground = Brushes.White;
            score.FontSize = 16;
            score.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetRow(score, 0);
            Grid.SetColumn(score, 1);

            grid.Children.Add(title);
            grid.Children.Add(score);
            return grid;
        }
    }
}
