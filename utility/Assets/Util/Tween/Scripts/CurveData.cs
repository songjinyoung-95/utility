using UnityEngine;

namespace Util.Tween
{
    [CreateAssetMenu(fileName = "CurveType", menuName = "Util/Tween")]
    public class CurveData : ScriptableObject
    {
        public ECurveType Type => _type;
        public AnimationCurve AnimationCurve =>_animationCurve;

        [SerializeField] private ECurveType _type;
        [SerializeField] private AnimationCurve _animationCurve;
    }
}