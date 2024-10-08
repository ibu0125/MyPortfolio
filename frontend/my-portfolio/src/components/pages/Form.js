import React from "react";
import "./Form.css";
import { useForm } from "react-hook-form";
import axios from "axios";

function Form() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();

  const onsubmit = async (data) => {
    console.log("Form Data:", data); // 入力データをログに表示
    const user = {
      Name: data.name,
      Email: data.email,
      Message: data.message,
    };

    try {
      const response = await axios.post(
        "http://localhost:5201/api/form/submit",
        user,
        {
          headers: {
            "Content-Type": "application/json", // Content-Typeを設定
          },
        }
      );
      console.log("Response:", response.data); // レスポンスを確認
      alert(response.data.message);
    } catch (error) {
      console.error("Error occurred:", error); // エラーをコンソールに表示
      if (error.response) {
        console.error("Error response data:", error.response.data); // エラーレスポンスを表示
        alert(`エラー: ${error.response.data.message}`);
      } else {
        alert("ネットワークエラーが発生しました。");
      }
    }
  };

  return (
    <div className="form">
      <div className="form-container">
        <h1>お問い合わせ</h1>
        <form onSubmit={handleSubmit(onsubmit)}>
          <label htmlFor="name">名前</label>
          <input
            id="name"
            type="text"
            placeholder="例: 山田 太郎" // ここでヒントテキストを指定
            {...register("name", { required: "※名前は必須です" })}
          />
          <p className="error-message">{errors.name?.message}</p>

          <label htmlFor="email">メールアドレス</label>
          <input
            id="email"
            type="email"
            placeholder="例: test@example.com" // ここでヒントテキストを指定
            {...register("email", { required: "※メールアドレスは必須です" })}
          />
          <p className="error-message">{errors.email?.message}</p>

          <label htmlFor="message">お問い合わせ</label>
          <textarea
            id="message"
            placeholder="内容をこちらに入力してください" // ここでヒントテキストを指定
            {...register("message", {
              required: "※お問い合わせ内容は必須です",
            })}
            style={{ height: "100px", width: "100%" }}
          />
          <p className="error-message">{errors.message?.message}</p>

          <button type="submit">送信</button>
        </form>
      </div>
    </div>
  );
}

export default Form;
