﻿using System.Reflection;
using System.Text.Json;
using CrazyCards.Application.Core.Cards.Commands;
using CrazyCards.Application.Core.Cards.Commands.CreateCard;
using CrazyCards.Domain.Entities.Card;

namespace CrazyCards.Application.Contracts.Cards;

public static class CardFactory
{
    public static Card CreateCardFromRequest(CreateCardCommand request)
    {
        Card card;

        // TOOD - Fix this switch statement
        card = request.Type! switch
        {
            { Value: 1 } => CreateMinionCard(request),
            { Value: 2 } => CreateSpellCard(request),
            _ => throw new NotSupportedException("Tipo de carta não suportado")
        };

        card.ManaCost = request.ManaCost;
        card.Name = request.Name;
        card.Description = request.Description;
        card.ImageId = request.ImageId;
        card.SkinId = request.SkinId;
        card.ClassId = request.ClassId;
        card.Rarity = request.Rarity!;

        FillAdditionalProperties(card, request.AdditionalProperties);

        return card;
    }

    private static MinionCard CreateMinionCard(CreateCardCommand request)
    {
        var minionCard = new MinionCard();

        if (request.AdditionalProperties.TryGetValue(nameof(MinionCard.Attack), out var attackProperty))
        {
            var attack = (JsonElement)attackProperty;
            minionCard.Attack = attack.GetUInt16();
        }
        if (request.AdditionalProperties.TryGetValue(nameof(MinionCard.Health), out var healthProperty))
        {
            var health = (JsonElement)healthProperty;
            minionCard.Health = health.GetUInt16();
        }

        return minionCard;
    }

    private static SpellCard CreateSpellCard(CreateCardCommand request)
    {
        var spellCard = new SpellCard();
        
        if (request.AdditionalProperties.TryGetValue(nameof(SpellCard.Damage), out var damageProperty))
        {
            var damage = (JsonElement)damageProperty;
            spellCard.Damage = damage.GetUInt16();
        }
        if (request.AdditionalProperties.TryGetValue(nameof(SpellCard.Heal), out var healProperty))
        {
            var heal = (JsonElement)healProperty;
            spellCard.Heal = heal.GetUInt16();
        }

        return spellCard;
    }

    private static void FillAdditionalProperties(Card card, IDictionary<string, object> additionalProperties)
    {
        foreach (var property in additionalProperties)
        {
            if (card.GetType().GetProperty(property.Key) is PropertyInfo cardProperty)
            {
                if (cardProperty.PropertyType == property.Value.GetType())
                {
                    cardProperty.SetValue(card, property.Value);
                }
            }
        }
    }
}
