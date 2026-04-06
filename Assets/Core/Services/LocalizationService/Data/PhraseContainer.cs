using System;

namespace Core.Services.LocalizationService.Data
{
  [Serializable]
  public struct PhraseContainer
  {
    public LocalizationPhraseKey Key;
    public string Line;
  }
}
