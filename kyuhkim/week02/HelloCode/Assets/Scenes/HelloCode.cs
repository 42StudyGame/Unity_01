using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



namespace Scenes
{
    public class HelloCode : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            Debug.Log("------------------------------------------");
            HelloWorld();
            Debug.Log("------------------------------------------");
            IntroduceSelf();
            Debug.Log("------------------------------------------");
            Distance();
            Debug.Log("------------------------------------------");
            Hentai(Random.Range(0, 100 + 1));
            Debug.Log("------------------------------------------");
            Age(Random.Range(0, 100 + 1));
            Debug.Log("------------------------------------------");
            Turn();
            Debug.Log("------------------------------------------");
            Loop();
            Debug.Log("------------------------------------------");
            LifetimeLoop();
            Debug.Log("------------------------------------------");
            Array();
            Debug.Log("------------------------------------------");
        }

        private static void HelloWorld()
        {
            Debug.Log("Hello world!");
        }

        private static void IntroduceSelf()
        {
            const string characterName = "noname";
            const char bloodType = 'A';
            const int age = 47;
            const float height = 180.0f;
            const bool isFemale = false;

            Debug.Log($"캐릭터 이름 : {characterName}");
            Debug.Log($"혈액형 : {bloodType}");
            Debug.Log($"나이 : {age}");
            Debug.Log($"키 : {height}");
            Debug.Log($"여성인가? : {isFemale}");
        }

        private static void Distance()
        {
            Debug.Log($"4 numbers = {GetDistance(1, 2, 3, 4)}");
            Debug.Log($"2 vectors = {GetDistance(new Vector2(1, 2), new Vector2(3, 4))}");
            Debug.Log($"2 tuples  = {GetDistance((1, 2), (3, 4))}");
        }

        private static void Hentai(int love)
        {
            Debug.Log($"passed love = {love}");
            switch (love)
            {
                case > 90:
                    Debug.Log("트루엔딩: 히로인과 결혼했다!");
                    break;
                case > 70:
                    Debug.Log("굿엔딩: 히로인과 사귀게 되었다!");
                    break;
                default:
                    Debug.Log("배드엔딩: 히로인에게 차였다.");
                    break;
            }
        }

        private static void Age(int age)
        {
            Debug.Log($"passed age = {age}");
            if (age is > 7 and < 18) {
                Debug.Log("의무 교육을 받고 있습니다."); }
            if (age is < 13 or > 70) {
                Debug.Log("일을 할 수 없는 나이입니다."); }
        }

        private static void Turn()
        {
            for (var i = 0; i < 10; ++i)
            {
                Debug.Log($"{i} 번째 순번입니다");
            }
        }

        private static void Loop()
        {
            var i = 0;
            while (i < 10) 
            {
                Debug.Log(i + " 번째 루프입니다.");
                i++;
            }
        }

        private static void LifetimeLoop()
        {
            var hp = 100;
            
            while (hp > 0) 
            {
                Debug.Log($"현재 체력 : {hp}");
                hp -= 33;
            }
            Debug.Log("플레이어는 죽었습니다.");
        }

        private static void Array()
        {
            var student = new int[5];
            
            PutArray(student);
            PrintArray(student);
        }

        private static void PutArray(IList<int> array)
        {
            for (var i = 0; i < array.Count; ++i)
            {
                array[i] = 100 - i * 10;
            }
        }

        private static void PrintArray(IReadOnlyList<int> array)
        {
            for (var i = 0; i < array.Count; ++i)
            {
                Debug.Log($"{i + 1}번 학생의 점수: {array[i]}");
            }
        }
        
        private static float GetDistance(float x1, float y1, float x2, float y2)
        {
            var width = x2 - x1;
            var height = y2 - y1;

            var distance = width * width + height * height;
            return Mathf.Sqrt(distance);
        }

        private static float GetDistance((float x, float y) pos1, (float x, float y) pos2)
        {
            (float width, float height) offset = (pos2.x - pos1.x, pos2.y - pos1.y);

            var distance = Mathf.Pow(offset.width, 2) + Mathf.Pow(offset.height, 2);
            return Mathf.Sqrt(distance);
        }

        private static float GetDistance(Vector2 v1, Vector2 v2) => Vector2.Distance(v1, v2);
    }
}
