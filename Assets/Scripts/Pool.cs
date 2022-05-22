using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private List<T> _pool;
    private T _prefab;
    private Transform _container;
    private bool _autoExpand = true;

    public Pool(T prefab, int count, Transform container, bool autoExpand = true)
    {
        this._prefab = prefab;
        this._autoExpand = autoExpand;
        this._container = container;
        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>();
        for (int i = 0; i < count; i++)
        {
            CreateAndAddObject();
        }
    }

    // Создаём объект, заносим его в пул и получаем его
    private T CreateAndAddObject(bool isActiveByDefault = false)
    {
        T element = GameObject.Instantiate(_prefab, _container);
        element.gameObject.SetActive(isActiveByDefault);
        _pool.Add(element);

        return element;
    }

    public bool HasFreeElement(out T element)
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (_pool[i] == null)
            {
                throw new System.Exception("Pool element was destroyed");
            }
            if (_pool[i].gameObject.activeInHierarchy == false)
            {
                element = _pool[i];
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetElement()
    {
        T element;
        if (HasFreeElement(out element) == false)
        {
            if (_autoExpand)
            {
                return CreateAndAddObject(isActiveByDefault: false);
            }
            else
            {
                throw new System.Exception($"There are no free elements in pool of type {typeof(T)}  ");
            }
        }

        return element;
    }
}