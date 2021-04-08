using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Tamagotchi
{
    class RankingFactory
    {
        public static void PopulateRanking(Panel panel, List<PlayerData> data)
        {
            panel.Children.Clear();
            data.Sort((a,b) => a.BestScore.Seconds - b.BestScore.Seconds);
            foreach (var x in data)
                panel.Children.Add(GetEntry(x));
        }

        public static Grid GetEntry(PlayerData data)
        {
            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            Label title = new Label();
            title.Content = data.Name;
            Grid.SetRow(title, 0);
            Grid.SetColumn(title, 0);

            Label score = new Label();
            score.Content = data.BestScore.ToString();
            score.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetRow(score, 0);
            Grid.SetColumn(score, 1);

            grid.Children.Add(title);
            grid.Children.Add(score);
            return grid;
        }
    }
}
