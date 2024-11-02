namespace OrbitaChallengeBackEnd.Models;

public class BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Data de inclusão
    public DateTime? UpdatedAt { get; set; } // Data de alteração
    public bool IsActive { get; set; } = true; // Ativo/Inativo
}

