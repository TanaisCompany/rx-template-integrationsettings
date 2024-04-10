using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.IntegrationCore.DocumentSyncSetting;

namespace Tanais.IntegrationCore
{
  partial class DocumentSyncSettingDocKindMatchingSharedHandlers
  {

    public virtual void DocKindMatchingKindChanged(Tanais.IntegrationCore.Shared.DocumentSyncSettingDocKindMatchingKindChangedEventArgs e)
    {
      _obj.KindGuid = e.NewValue?.Id;
    }
  }

  partial class DocumentSyncSettingSharedHandlers
  {

    public virtual void DocumentTypeChanged(Tanais.IntegrationCore.Shared.DocumentSyncSettingDocumentTypeChangedEventArgs e)
    {
      _obj.DocumentTypeGuid = e.NewValue?.Id.ToString();
    }

  }
}