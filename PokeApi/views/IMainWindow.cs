
using System.Windows;

namespace PokeApi.views
{
    public interface IMainWindow
    {
        void showError(string msg);
        void showLoading(bool visible);
    }   
}
