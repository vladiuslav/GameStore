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
    window.location.reload();
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
        Add Comment
      </button>
    </div>
  );
};

export default GameCreateComment;
