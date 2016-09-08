using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nerdytinder
{
    class PersonViewModel: ViewModelBase
    {
        string _personId;

        public string PersonId
        {
            get
            {
                return _personId;
            }
            set
            {
                if (_personId != value)
                {
                    _personId = value;
                    OnPropertyChanged("PersonId");
                }
            }
        }

        Person _person;

        public Person Person
        {
            get
            {
                if(_person == null && PersonId != null)
                {
                    Task.Run(async () =>
                    {
                        _person = await AzureService.Instance.PersonManager.Table.LookupAsync(PersonId);
                    }).Wait();
                }
                return _person;
            }
        }
    }
}
