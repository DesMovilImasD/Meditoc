using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace CallCenter.ViewModels
{
    class ChatViewModel : BaseViewModel
    {
        private ObservableCollection<MessageViewModel> messagesList;

        public ObservableCollection<MessageViewModel> Messages
        {
            get { return messagesList; }
            set { messagesList = value; RaisePropertyChanged(); }
        }

        private string outgoingText;

        public string OutGoingText
        {
            get { return outgoingText; }
            set { outgoingText = value; RaisePropertyChanged(); }
        }

        public ICommand SendCommand { get; set; }

        public ChatViewModel()
        {
            Messages = new ObservableCollection<MessageViewModel>();
        }


        double heigthmain = 500;
        public const string heigthmainPropertyName = "Heigthmain";
        public double Heigthmain
        {
            get { return heigthmain; }
            set { SetProperty(ref heigthmain, value, heigthmainPropertyName); }
        }

    }
}
