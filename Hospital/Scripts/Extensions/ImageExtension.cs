namespace Muhe.Mjhx.Extensions
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 用于<see cref="Image"/>的扩展。
    /// </summary>
    public static class ImageExtension
    {
        /// <summary>
        /// 设置图片的灰度级别。
        /// </summary>
        /// <param name="obj"><see cref="Image"/>组件所附加的对象</param>
        /// <param name="grayscale">灰度级别，范围为[0,1]</param>
        public static void SetGrayscale(this GameObject obj, float grayscale)
        {
            var images = obj.GetComponentsInChildren<Image>();
            if (images.Length > 0)
            {
                foreach (var image in images)
                {
                    if (image == null)
                        return;
                    if (image.material == null || image.material.shader != mGrayscaleShader)
                        image.material = new Material(mGrayscaleShader);
                    //            image.material.SetFloat("GrayScale", grayscale);
                }
            }
        }

        /// <summary>
        /// 设置图片的灰度级别。
        /// </summary>
        /// <param name="obj"><see cref="Image"/>组件所附加的对象</param>
        /// <param name="isGray">是否设置为灰</param>
        public static void SetGray(this GameObject obj, bool isGray=true)
        {
            var images = obj.GetComponentsInChildren<Image>();
            if (images.Length > 0)
            {
                foreach (var image in images)
                {
                    if (image == null)
                        return;
                    if (isGray)
                    {
                        if (image.material == null || image.material.shader != mGrayscaleShader)
                            image.material = new Material(mGrayscaleShader);
                    }
                    else
                    {
                        if (image.material != null)
                        {
                            //image.material.shader = null;
                            image.material = null;
                        }   
                    }
                }
            }
        }

        /// <summary>
        /// 设置gameobject为暗色，表示不可用
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="color"></param>
        /// <param name="textColor"></param>
        public static void SetDisable(this GameObject obj,Color color=default(Color),Color textColor=default(Color))
        {
            if(color==default(Color))
                color=new Color(129f/255,129f/255,129f/255,1f);   //默认颜色
            if (textColor == default(Color))
                textColor = new Color(129f / 255, 129f / 255, 129f / 255, 1f);   //文本默认颜色
            setGameObjectStatus(obj, color,textColor);
        }

        /// <summary>
        /// 恢复正常颜色状态，表示可用
        /// </summary>
        /// <param name="obj"></param>
        public static void SetEnable(this GameObject obj)
        {
            var color = Color.white;   //默认颜色
            setGameObjectStatus(obj, color,color);
        }

        private static void setGameObjectStatus(GameObject go, Color color, Color textColor = default(Color))
        {
            var images = go.GetComponentsInChildren<Image>();
            if (images.Length > 0)
            {
                foreach (var image in images)
                {
                    image.color = color;
                }
            }

            var texts = go.GetComponentsInChildren<Text>();
            if (texts.Length > 0)
            {
                foreach (var text in texts)
                {
                    text.color = textColor;
                }
            }
        }

        public static void SetBlur(this GameObject obj, float blur)
        {
            var image = obj.GetComponent<Image>();
            if (image == null)
                return;
            if (image.material == null || image.material.shader != mBlurShader)
                image.material = new Material(mImageBlurShader);
            //image.material.SetFloat("BlurSizeXY", blur);
        }

        private static readonly Shader mGrayscaleShader = Shader.Find("Mjhx/UI/Gray");
        private static readonly Shader mBlurShader = Shader.Find("Mjhx/UI/Blur");
        private static readonly Shader mImageBlurShader = Shader.Find("Mjhx/UI/ImageBlur");
    }
}