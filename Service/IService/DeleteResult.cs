// Dentro do projeto Service/IServices/
// Arquivo: DeleteResult.cs

namespace Service.IServices
{
    // O Enum para comunicar o resultado da exclusão
    public enum DeleteResult
    {
        Success,      // Recurso ativamente excluído
        NotFound      // Recurso já estava ausente
    }
}