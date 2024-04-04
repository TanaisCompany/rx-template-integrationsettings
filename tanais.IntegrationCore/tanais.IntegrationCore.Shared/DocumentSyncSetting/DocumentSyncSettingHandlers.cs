using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using tanais.IntegrationCore.DocumentSyncSetting;

namespace tanais.IntegrationCore
{
  partial class DocumentSyncSettingDocKindMatchingSharedHandlers
  {

    public virtual void DocKindMatchingKindChanged(tanais.IntegrationCore.Shared.DocumentSyncSettingDocKindMatchingKindChangedEventArgs e)
    {
      _obj.KindGuid = e.NewValue?.Id;
    }
  }

  partial class DocumentSyncSettingSharedHandlers
  {

    public virtual void DocumentTypeChanged(tanais.IntegrationCore.Shared.DocumentSyncSettingDocumentTypeChangedEventArgs e)
    {
      _obj.DocumentTypeGuid = e.NewValue?.Id.ToString();
    }

  }
}