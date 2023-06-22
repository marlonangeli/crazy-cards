using CrazyCards.Domain.Primitives;

namespace CrazyCards.Application.Validation;

internal static partial class ValidationErrors
{
    internal static class Hability
    {
        internal static Error CardDoesNotExist => new(
            "Hability.CardDoesNotExist",
            "A carta não existe.");
        
        internal static Error TypeDoesNotExist => new(
            "Hability.TypeDoesNotExist",
            "O tipo de habilidade não existe.");
        
        internal static Error ActionDescriptionIsNullOrEmpty => new(
            "Hability.Action.DescriptionIsNullOrEmpty",
            "A descrição da ação não pode ser nula ou vazia.");
        
        internal static Error ActionDescriptionIsTooLong => new(
            "Hability.Action.DescriptionIsTooLong",
            "A descrição da ação não pode ter mais que 512 caracteres.");
        
        internal static Error ActionInvokeCardDoesNotExist => new(
            "Hability.Action.InvokeCardDoesNotExist",
            "A carta invocada não existe.");
        
        internal static Error ActionInvokeCardTypeDoesNotExist => new(
            "Hability.Action.InvokeCardTypeDoesNotExist",
            "O tipo da carta invocada não existe.");
        
        internal static Error ActionDamageIsNegative => new(
            "Hability.Action.DamageIsNegative",
            "O dano da ação não pode ser negativo.");
        
        internal static Error ActionHealIsNegative => new(
            "Hability.Action.HealIsNegative",
            "A cura da ação não pode ser negativa.");
        
        internal static Error ActionShieldIsNegative => new(
            "Hability.Action.ShieldIsNegative",
            "O escudo da ação não pode ser negativo.");
    }
}