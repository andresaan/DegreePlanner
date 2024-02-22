using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Data;
using CommunityToolkit.Mvvm.Input;
using DegreePlanner.View;
using ApplicationCore.Interfaces;

namespace DegreePlanner.ViewModel
{
    public partial class DegreePlanViewModel : ObservableObject
    {
        private ITermService _termService;


        [ObservableProperty]
        public ObservableCollection<Term> terms;

        [ObservableProperty]
        public string termName;

        [ObservableProperty]
        public DateTime start;

        [ObservableProperty]
        public DateTime end;

        public DegreePlanViewModel(ITermService termService)
        {
            _termService = termService;

        }

        [RelayCommand]
        async Task AddCourse(int termId)
        {
            await Shell.Current.GoToAsync($"{nameof(AddEditCourseView)}?TermId={termId}");
        }

        [RelayCommand]
        public void AddTerm()
        {
            _termService.AddItem<Term>(new Term()
            {
                Name = TermName,
                Start = Start,
                End = End,
                TotalCus = 0
            });

            LoadTerms();
        }

        public void LoadTerms()
        {
            
            Terms = new ObservableCollection<Term>(_termService.GetAllTerms());

            //Add a formatting call using model to format OA and PA and other things as they come up
        }
    }
}
