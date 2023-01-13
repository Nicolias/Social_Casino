using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private CreditPanel _creditPanel;
    [SerializeField] private BetPanel _betPanel;
    [SerializeField] private WinWindow _winWindow;
    
    public override void InstallBindings()
    {
        Container.Bind<CreditPanel>().FromComponentOn(_creditPanel.gameObject).AsSingle();
        Container.Bind<WinWindow>().FromComponentOn(_winWindow.gameObject).AsSingle();
        Container.Bind<CombinationInterpreter>().FromNew().AsSingle();
        Container.Bind<WinRateGenerator>().FromNew().AsSingle();
        Container.Bind<BetPanel>().FromComponentOn(_betPanel.gameObject).AsSingle();
    }
}
