using System.Text.Json;

using Json.Schema;

string json = @"
{
    ""$schema"": ""https://json-schema.org/draft/2020-12/schema"",
    ""$id"": ""https://localhost:7289/v1/Schemas/Character"",
    ""title"": ""Character"",
    ""type"": ""object"",
    ""properties"": {
        ""Strength"": {
            ""$ref"": ""#/definitions/Ability"",
            ""description"": ""Strength measures bodily power, athletic training, and the extent to which you can exert raw physical force.""
        },
        ""Dexterity"": {
            ""$ref"": ""#/definitions/Ability"",
            ""description"": ""Dexterity measures agility, reflexes, and balance.""
        },
        ""Constitution"": {
            ""$ref"": ""#/definitions/Ability"",
            ""description"": ""Constitution measures health, stamina, and vital force.""
        },
        ""Intelligence"": {
            ""$ref"": ""#/definitions/Ability"",
            ""description"": ""Intelligence measures mental acuity, accuracy of recall, and the ability to reason.""
        },
        ""Wisdom"": {
            ""$ref"": ""#/definitions/Ability"",
            ""description"": ""Wisdom reflects how attuned you are to the world around you and represents perceptiveness and intuition.""
        },
        ""Charisma"": {
            ""$ref"": ""#/definitions/Ability"",
            ""description"": ""Charisma measures your ability to interact effectively with others. It includes such factors as confidence and eloquence, and it can represent a charming or commanding personality.""
        },
        ""Defences"": {
            ""type"": ""object"",
            ""properties"": {
                ""Armour"": {
                    ""$ref"": ""#/definitions/Defence""
                },
                ""Strength"": {
                    ""$ref"": ""#/definitions/Defence""
                },
                ""Dexterity"": {
                    ""$ref"": ""#/definitions/Defence""
                },
                ""Constitution"": {
                    ""$ref"": ""#/definitions/Defence""
                },
                ""Intelligence"": {
                    ""$ref"": ""#/definitions/Defence""
                },
                ""Wisdom"": {
                    ""$ref"": ""#/definitions/Defence""
                },
                ""Charisma"": {
                    ""$ref"": ""#/definitions/Defence""
                }
            }
        },
        ""Resistances"": {
            ""type"": ""object"",
            ""properties"": {
                ""Bludgeoning"": {
                    ""$ref"": ""#/definitions/Resistance""
                },
                ""Piercing"": {
                    ""$ref"": ""#/definitions/Resistance""
                },
                ""Slashing"": {
                    ""$ref"": ""#/definitions/Resistance""
                },
                ""Acid"": {
                    ""$ref"": ""#/definitions/Resistance""
                },
                ""Cold"": {
                    ""$ref"": ""#/definitions/Resistance""
                },
                ""Fire"": {
                    ""$ref"": ""#/definitions/Resistance""
                },
                ""Force"": {
                    ""$ref"": ""#/definitions/Resistance""
                },
                ""Lightning"": {
                    ""$ref"": ""#/definitions/Resistance""
                },
                ""Necrotic"": {
                    ""$ref"": ""#/definitions/Resistance""
                },
                ""Poison"": {
                    ""$ref"": ""#/definitions/Resistance""
                },
                ""Psychic"": {
                    ""$ref"": ""#/definitions/Resistance""
                },
                ""Radiant"": {
                    ""$ref"": ""#/definitions/Resistance""
                },
                ""Thunder"": {
                    ""$ref"": ""#/definitions/Resistance""
                }
            }
        },
        ""Resources"": {
            ""type"": ""object"",
            ""properties"": {
                ""HitPoints"": {
                    ""$ref"": ""#/definitions/Resource""
                }
            }
        },
        ""Skills"": {
            ""type"": ""object"",
            ""properties"": {
                ""Acrobatics"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""AnimalHandling"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""Arcana"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""Athletics"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""Deception"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""History"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""Insight"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""Intimidation"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""Investigation"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""Medicine"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""Nature"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""Perception"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""Performance"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""Persuasion"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""Religion"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""SleightOfHand"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""Stealth"": {
                    ""$ref"": ""#/definitions/Skill""
                },
                ""Survival"": {
                    ""$ref"": ""#/definitions/Skill""
                }
            }
        }
    },
    ""required"": [
        ""Strength"",
        ""Dexterity"",
        ""Constitution"",
        ""Intelligence"",
        ""Wisdom"",
        ""Charisma""
    ],
    ""definitions"": {
        ""Ability"": {
            ""type"": ""integer"",
            ""default"": 8,
            ""minimum"": 1,
            ""maximum"": 30
        },
        ""Defence"": {
            ""type"": ""object"",
            ""properties"": {
                ""Modifier"": {
                    ""enum"": [
                        ""None"",
                        ""Advantage"",
                        ""Disadvantage""
                    ]
                },
                ""Value"": {
                    ""type"": ""integer""
                }
            },
            ""required"": [
                ""Value""
            ]
        },
        ""Resistance"": {
            ""enum"": [
                ""None"",
                ""Vulnerable"",
                ""Resistant"",
                ""Immune""
            ]
        },
        ""Resource"": {
            ""type"": ""object"",
            ""properties"": {
                ""Current"": {
                    ""type"": ""integer"",
                    ""minimum"": 0
                },
                ""Max"": {
                    ""type"": ""integer"",
                    ""minimum"": 0
                }
            },
            ""required"": [
                ""Current"",
                ""Max""
            ]
        },
        ""Skill"": {
            ""type"": ""object"",
            ""properties"": {
                ""Modifier"": {
                    ""enum"": [
                        ""None"",
                        ""Advantage"",
                        ""Disadvantage""
                    ]
                },
                ""Value"": {
                    ""type"": ""integer""
                }
            },
            ""required"": [
                ""Value""
            ]
        }
    }
}";

JsonSchema? schema = JsonSchema.FromText(json);

JsonDocument document = JsonDocument.Parse(@"
{
    ""Strength"": 8,
    ""Dexterity"": 8,
    ""Constitution"": 8,
    ""Intelligence"": 8,
    ""Wisdom"": 8,
    ""Charisma"": 8
}");

ValidationResults? result = schema?.Validate(document.RootElement, new ValidationOptions
{
    OutputFormat = OutputFormat.Verbose,
});

Console.WriteLine();
