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
    <div>
      <label>
        Comment:
        <input
          type="text"
          value={commentText}
          onChange={(event) => setCommentText(event.target.value)}
        />
      </label>
      <button
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
