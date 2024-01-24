using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using tanais.IntegrationCore.IntegratedSystem;

namespace tanais.IntegrationCore
{
  partial class IntegratedSystemClientHandlers
  {

    public override void Showing(Sungero.Presentation.FormShowingEventArgs e)
    {
      _obj.State.Properties.Code.IsEnabled = _obj.State.IsInserted;
    }

  }
}