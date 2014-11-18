using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace BrewDay.WPF.Client
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(int color)
        {
            Ingredients = new ObservableCollection<Ingredient>();
            AddIngredientCmd = new RelayCommand(p =>
            {
                if (!_added.Contains(SelectedIngredient))
                {
                    _added.Add(SelectedIngredient);
                    //recalc srm color
                    //BeerBuilder.Functions.GetSRMDisplayColor()
                }
            });

            var bytes = BitConverter.GetBytes(color);
            var dispColor = Color.FromRgb(bytes[3], bytes[2], bytes[1]);
            var colorString = string.Format("#{0:X2}{1:X2}{2:X2}", bytes[2], bytes[1], bytes[0]);
            this.SRMColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorString));
        }

        public SolidColorBrush SRMColor { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        List<Ingredient> _added = new List<Ingredient>();
        public RelayCommand AddIngredientCmd { get; private set; }

        Ingredient _selected;
        public Ingredient SelectedIngredient 
        { 
            get {return _selected; }
            set
            {
                _selected = value;
            }
        }

        public async void GetIngredients()
        {
            using (var http = new System.Net.Http.HttpClient())
            {
                http.BaseAddress = new Uri("http://localhost:7844/");
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                System.Net.Http.HttpResponseMessage  response = await http.GetAsync("brewDayAPI/ingredients/");
                if(response.IsSuccessStatusCode)
                {
                    var ingredients = await response.Content.ReadAsStringAsync();
                    var ingObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Ingredient>>(ingredients);
                    foreach (var item in ingObjects)
                    {
                        Ingredients.Add(item);
                    }
                }


                //var ings = await _api.GetIngredients();
                //Console.WriteLine(ings);
                
            }

        }

    }
}
