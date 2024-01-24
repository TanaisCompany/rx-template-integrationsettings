using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using tanais.IntegrationCore.IntegrationSetting;

namespace tanais.IntegrationCore
{
  partial class IntegrationSettingSharedHandlers
  {

    public virtual void BusinessUnitChanged(tanais.IntegrationCore.Shared.IntegrationSettingBusinessUnitChangedEventArgs e)
    {
      tanais.IntegrationCore.Functions.IntegrationSetting.SetIntegrationSettingsName(_obj);
    }

    public virtual void IntegratedSystemChanged(tanais.IntegrationCore.Shared.IntegrationSettingIntegratedSystemChangedEventArgs e)
    {
      tanais.IntegrationCore.Functions.IntegrationSetting.SetIntegrationSettingsName(_obj);
    }

    public virtual void AuthMethodChanged(Sungero.Domain.Shared.EnumerationPropertyChangedEventArgs e)
    {
      tanais.IntegrationCore.Functions.IntegrationSetting.SetStateProperties(_obj);
    }

  }
}