using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public static class Generic
{
    public static IEnumerator Fade(float targetAlpha, Text text, float transitionTime, bool gameTime)
    {
        while(text.color.a != targetAlpha)
        {
            Color temp = text.color;
            float interval = gameTime ? Time.deltaTime : Time.unscaledDeltaTime;
            temp.a = Mathf.MoveTowards(temp.a, targetAlpha, 1 / transitionTime * interval);
            text.color = temp;
            yield return null;
        }
    }
    
    public static IEnumerator Fade(float targetAlpha, Image image, float transitionTime, bool gameTime)
    {
        while(image.color.a != targetAlpha)
        {
            Color temp = image.color;
            float interval = gameTime ? Time.deltaTime : Time.unscaledDeltaTime;
            temp.a = Mathf.MoveTowards(temp.a, targetAlpha, 1 / transitionTime * interval);
            image.color = temp;
            yield return null;
        }
    }
    public static IEnumerator Fade(float targetAlpha, SpriteRenderer image, float transitionTime, bool gameTime)
    {
        while(image.color.a != targetAlpha)
        {
            Color temp = image.color;
            float interval = gameTime ? Time.deltaTime : Time.unscaledDeltaTime;
            temp.a = Mathf.MoveTowards(temp.a, targetAlpha, 1 / transitionTime * interval);
            image.color = temp;
            yield return null;
        }
    }

    public static float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    public static float SetRandomOffset(float origin, float offset)
    {
        return Random.Range(origin - offset, origin + offset);
    }
    public static Vector3 SetRandomCircularPosition(Vector3 position, float offset)
    {
        position.x = SetRandomOffset(position.x, offset);
        position.y = SetRandomOffset(position.y, offset);
        return position;
    }
}
