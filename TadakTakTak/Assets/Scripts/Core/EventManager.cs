using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Main.Event
{
    public class EventManager : MonoSingleton<EventManager>
    {

        private Dictionary<EventsType, Action> eventDictionary = new Dictionary<EventsType, Action>();
        private Dictionary<EventsType, Action<object>> eventParamDictionary = new Dictionary<EventsType, Action<object>>();

        private void Start()
        {
            StartListening(EventsType.ClearEvents, ClearEvents);
        }

        /// <summary>
        /// �̺�Ʈ �Լ� ����ϱ� 
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="listener"></param>
        public void StartListening(EventsType eventName, Action listener)
        {
            Action thisEvent;
            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                //���� �̺�Ʈ�� �� ���� �̺�Ʈ �߰� 
                thisEvent += listener;

                //��ųʸ� ������Ʈ
                Instance.eventDictionary[eventName] = thisEvent;
            }
            else
            {
                //ó������ ��ųʸ��� �̺�Ʈ �߰� 
                thisEvent += listener;
                Instance.eventDictionary.Add(eventName, thisEvent);
            }
        }
        public void StartListening(EventsType eventName, Action<object> listener)
        {
            Action<object> thisEvent;

            if (Instance.eventParamDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent += listener;
                Instance.eventParamDictionary[eventName] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                Instance.eventParamDictionary.Add(eventName, listener);
            }
        }

        /// <summary>
        /// �̺�Ʈ �Լ� �����ϱ� 
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="listener"></param>
        public void StopListening(EventsType eventName, Action listener)
        {
            if (Instance == null)
            {
                return;
            }
            Action thisEvent;
            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                //���� �̺�Ʈ���� �̺�Ʈ ����
                thisEvent -= listener;

                //�̺�Ʈ ������Ʈ 
                Instance.eventDictionary[eventName] = thisEvent;
            }

        }

        public void StopListening(EventsType eventName, Action<object> listener)
        {
            if (Instance == null)
            {
                return;
            }
            Action<object> thisEvent;
            if (Instance.eventParamDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent -= listener;

                Instance.eventParamDictionary[eventName] = thisEvent;
            }
        }
        /// <summary>
        /// �̺�Ʈ �Լ� ���� 
        /// </summary>
        /// <param name="eventName"></param>
        public void TriggerEvent(EventsType eventName)
        {
            Action thisEvent = null;
            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                Debug.Log("�̺�Ʈ ����!");
                thisEvent?.Invoke();
            }
            else
            {
                Debug.LogError("�� �̺�Ʈ�Դϴ�");
            }
        }

        public void TriggerEvent(EventsType eventName, object param)
        {
            Action<object> thisEvent = null;
            if (Instance.eventParamDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent?.Invoke(param);
            }
            else
            {
                Debug.LogError("�� �̺�Ʈ�Դϴ�");
            }
        }

        public void ClearEvents()
        {
            Instance.eventDictionary.Clear();
            Instance.eventParamDictionary.Clear();
            StartListening(EventsType.ClearEvents, ClearEvents);

        }
    }


}