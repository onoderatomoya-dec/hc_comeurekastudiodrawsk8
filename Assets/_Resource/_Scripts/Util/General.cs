using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class General : MonoBehaviour
{
    [SerializeField] private Animator[] _anims;
    [SerializeField] private string _firstAnimName;
    private List<SkinMeshController> _skinMeshControllerList = new List<SkinMeshController>();
    
    // スキン管理
    public class  SkinMeshController
    {
        private List<SkinnedMeshRenderer> _skinnedMeshRendererList;
        private List<MeshRenderer> _meshRendererList;
        public void Init(Animator animator)
        {
            _skinnedMeshRendererList = animator.GetComponentsInChildren<SkinnedMeshRenderer>().ToList();
            _meshRendererList = animator.GetComponentsInChildren<MeshRenderer>().ToList();
        }

        public void HideSkin()
        {
            foreach (var skinnedMeshRenderer in _skinnedMeshRendererList)
            {
                skinnedMeshRenderer.enabled = false;
            }
            foreach (var meshRenderer in _meshRendererList)
            {
                meshRenderer.enabled = false;
            }
        }

        public void ShowSkin()
        {
            foreach (var skinnedMeshRenderer in _skinnedMeshRendererList)
            {
                skinnedMeshRenderer.enabled = true;
            }
            foreach (var meshRenderer in _meshRendererList)
            {
                meshRenderer.enabled = true;
            }
        }
    }
    
    
    
    void Awake()
    {
        foreach (Animator animator in _anims)
        {
            SkinMeshController skinMeshController = new SkinMeshController();
            skinMeshController.Init(animator);
            _skinMeshControllerList.Add(skinMeshController);
        }
    }

    void Start()
    {
        if (_firstAnimName != "")
        {
            SetBools(_firstAnimName);
        }
    }

    public void SetBools(string name)
    {
        foreach(Animator anim in _anims)
        {
            foreach (var par in anim.parameters)
            {
                anim.SetBool(par.name, false);
            }
            anim.SetBool(name, true);
        }
        
    }

    public void SetTriggers(string name)
    {
        foreach (Animator anim in _anims)
        {
            anim.SetTrigger(name);
        }

    }

    // アニメーションのスピード変更
    public void SetAnimSpeed(float speed)
    {
        foreach (Animator anim in _anims)
        {
            anim.speed = speed;
        }
    }

    // タイムスケールを無視する
    public void OnUnscaledTime()
    {
        foreach (Animator anim in _anims)
        {
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
    }

    // 再生位置変更
    public void SetNormalizedTime(float time)
    {
        foreach (Animator anim in _anims)
        {
            anim.ForceStateNormalizedTime(time);
        }
    }
    
    // 全てのスキンを非表示に
    public void AllHideSkin()
    {
        foreach (SkinMeshController skinMeshController in _skinMeshControllerList)
        {
            skinMeshController.HideSkin();
        }
    }
    
    // 指定のスキンを表示
    public void ShowSkin(int index)
    {
        _skinMeshControllerList[index].ShowSkin();
    }
}
