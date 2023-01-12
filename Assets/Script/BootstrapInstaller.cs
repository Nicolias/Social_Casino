using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private CreditPanel _creditPanel;

    public override void InstallBindings()
    {
        Container.Bind<CreditPanel>().FromComponentOn(_creditPanel.gameObject).AsSingle();
        Container.Bind<CombinationInterpreter>().FromNew().AsSingle();
        Container.Bind<WinRateGenerator>().FromNew().AsSingle();
    }
}
