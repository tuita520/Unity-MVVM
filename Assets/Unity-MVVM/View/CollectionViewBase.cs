﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMVVM.Binding;

namespace UnityMVVM
{
    namespace View
    {
        [RequireComponent(typeof(CollectionViewSource))]
        public class CollectionViewBase : MonoBehaviour
        {
            [SerializeField]
            protected GameObject _listItemPrefab;

            public List<GameObject> InstantiatedItems = new List<GameObject>();

            [SerializeField]
            protected CollectionViewSource _src;

            // Use this for initialization
            protected void Awake()
            {
                if (_src != null)
                {
                    _src.OnElementsAdded += AddElements;
                    _src.OnElementsRemoved += RemoveElements;
                    _src.OnCollectionReset += ResetView;
                    _src.OnElementUpdated += UpdateElement;
                }
            }

            protected virtual void UpdateElement(int index, IList newItems)
            {
                // Do Nothing
                //var toUpdate = InstantiatedItems[index];
                //InstantiatedItems.RemoveAt(index);

                //GameObject.Destroy(toUpdate);

                //AddElement(index, newValue);
            }

            protected void OnDestroy()
            {
                if (_src != null)
                {
                    _src.OnElementsAdded -= AddElements;
                    _src.OnElementsRemoved -= RemoveElements;
                    _src.OnCollectionReset -= ResetView;
                    _src.OnElementUpdated -= UpdateElement;
                }
            }

            protected virtual void InitItem(GameObject go, object item, int index)
            {

            }


            protected virtual void ResetView(int newStartingIndex, IList newItems)
            {
                foreach (Transform t in transform)
                    GameObject.Destroy(t.gameObject);

                InstantiatedItems.Clear();
            }

            //
            // Override this method to create the gameobject that will spawn in your CollectionView
            //
            protected virtual GameObject CreateCollectionItem(object ListItem, Transform parent)
            {
                var go = GameObject.Instantiate(_listItemPrefab, transform);

                return go;
            }

            protected virtual void AddElement(int index, object newItem)
            {
                var go = CreateCollectionItem(newItem, transform);
                go.transform.SetSiblingIndex(index);

                InstantiatedItems.Insert(index, go);
            }

            protected virtual void AddElements(int newStartingIndex, IList newItems)
            {
                int idx = 0;
                var gameObjects = new List<GameObject>(newItems.Count);
                foreach (var item in newItems)
                {
                    var go = CreateCollectionItem(item, transform);
                    go.transform.SetSiblingIndex(newStartingIndex);

                    gameObjects.Add(go);

                    InitItem(go, item, idx);
                    idx++;
                }

                InstantiatedItems.InsertRange(newStartingIndex, gameObjects);
            }

            protected virtual void RemoveElements(int oldStartingIndex, IList oldItems)
            {
                for (int i = oldStartingIndex; i < oldStartingIndex + oldItems.Count; i++)
                {
                    GameObject.Destroy(InstantiatedItems[i]);
                    InstantiatedItems[i] = null;
                }

                InstantiatedItems.RemoveRange(oldStartingIndex, oldItems.Count);
            }
            private void OnValidate()
            {
                _src = GetComponent<CollectionViewSource>();
            }
        }
    }
}

