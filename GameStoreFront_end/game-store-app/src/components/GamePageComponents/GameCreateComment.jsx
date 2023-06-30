import React, { useState } from "react";
import fetchCreateComment from "../Fetches/fetchComments/fetchCreateComment";

const GameCreateComment = (props) => {
  const [commentText, setCommentText] = useState("");

  const CreateComment = async () => {
    const result = await fetchCreateComment({
      gameId: props.gameId,
      commentText: commentText,
      repliedCommentId: props.repliedCommentId,
    });
    if (result.ok) {
      window.location.reload();
    } else {
      let errorBody = await result.json();
      alert(
        errorBody.title +
          "\n" +
          (errorBody.detail !== undefined ? errorBody.detail : "")
      );
      return;
    }
  };

  return (
    <div className="comment-form">
      <label>
        Comment:
        <input
          className="comment-text-input"
          type="text"
          value={commentText}
          onChange={(event) => setCommentText(event.target.value)}
        />
      </label>
      <button
        className="comment-button"
        onClick={() => {
          CreateComment();
        }}
      >
        Save
      </button>{" "}
      <button
        className="comment-button"
        onClick={() => {
          props.closeAddComment();
        }}
      >
        Close
      </button>
    </div>
  );
};

export default GameCreateComment;
