using Sandbox;
using System.ComponentModel.DataAnnotations;

public sealed class SnotComponent : Component
{
	[Property]
	public UnitInfo Info { get; set; }

	[Property]
	public SkinnedModelRenderer Model { get; set; }

	protected override void OnStart()
	{
		if ( Info != null )
			Info.OnDamage += HurtAnimation;
	}

	protected override void OnUpdate()
	{
		if (Info == null) return;
		if (Model == null) return;

		var currentHealth = Model.GetFloat( "health" );
		var scaledHealth = Info.Health / Info.MaxHealth * 100f;
		var lerpedHealth = MathX.Lerp( currentHealth, scaledHealth, Time.Delta / 0.1f );
		Model.Set( "health", lerpedHealth );
	}

	public void HurtAnimation( float damage )
	{
		var scaleDamage = damage / Info.MaxHealth * 100f;

		if ( Model != null ) 
		{
			Model?.Set( "damage", scaleDamage );
			Model?.Set( "hit", true );
		}
	}
}
