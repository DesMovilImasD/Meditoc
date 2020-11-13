namespace BC.CallCenter.Models.Interfaces
{
    public interface IGeometriaEstados
    {
        bool m_ActivarEstado(string sEstado);
        bool m_DesactivarEstado(string sEstado);
        bool m_EsValidaLaLocacion();
        bool m_EsValidaLaRegion();
    }
}
