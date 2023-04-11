import React from "react";
import { useState, useEffect } from "react";

import fetchUserGetCurrent from "../Fetches/fetchUsers/fetchUsersGet/fetchUserGetCurrent";
import fetchUserGetAll from "../Fetches/fetchUsers/fetchUsersGet/fetchUsersGetAll";
import fetchGetCommentsForGame from "../Fetches/fetchComments/fetchGetCommentsForGame";
import fetchUpdateComment from "../Fetches/fetchComments/fetchUpdateComment";
import fetchDeleteComment from "../Fetches/fetchComments/fetchDeleteComment";
import GetUserImage from "../userPageComponents/GetUserImage";
import GameCreateComment from "./GameCreateComment";
import CheckIsTokenExpired from "../JsFunctions/CheckIsTokenExpired";

const GameComments = (props) => {
  const [comments, setComments] = useState("");
  const [users, setUsers] = useState("");
  const [userCurrent, setCurrentUser] = useState({});
  const [changeCommentText, setChangeCommentText] = useState("");
  const [changeCommentId, setchangeCommentId] = useState(0);
  const [removeCommentId, setremoveCommentId] = useState(0);
  const [replyCommentId, setreplyCommentId] = useState(0);

  useEffect(() => {
    const getUsers = async () => {
      const result = await fetchUserGetAll();
      let resultJson = await result.json();
      setUsers(resultJson);
    };
    getUsers();

    const getComments = async () => {
      const result = await fetchGetCommentsForGame(props.gameId);
      if (result.status === 200) {
        let resultJson = await result.json();
        setComments(resultJson);
      }
    };
    getComments();

    const getCurrentUser = async () => {
      CheckIsTokenExpired();
      const token = localStorage.getItem("token");
      if (token !== null) {
        const result = await fetchUserGetCurrent(token);
        if (result.status === 200) {
          let resultjson = await result.json();
          setCurrentUser(resultjson);
        }
      }
    };
    getCurrentUser();
  }, []);

  const getCommentsWithReplied = function() {
    let newComents = [];
    comments.forEach((element) => {
      if (element.repliedCommentId === null) {
        newComents.push(element);
      } else {
        let id = newComents.findIndex((c) => c.id === element.repliedCommentId);
        newComents[id].repliedComment = element;
      }
    });
    return newComents;
  };

  const DeleteButton = function(comment) {
    if (removeCommentId === comment.id) {
      return (
        <span
          onClick={() => {
            setremoveCommentId(0);
          }}
        >
          Restore
        </span>
      );
    } else {
      return (
        <span
          onClick={() => {
            setremoveCommentId(comment.id);
          }}
        >
          Delete
        </span>
      );
    }
  };

  const updateComment = async () => {
    const result = await fetchUpdateComment({
      commentText: changeCommentText,
      commentId: changeCommentId,
    });

    if (result.status === 200) {
      window.location.reload();
    }
  };

  const renderComment = function(comment, replied = false) {
    let user = users.find((u) => u.id === comment.userId);

    return (
      <div className={replied ? "comment-replied" : "comment"}>
        <div className="comment-user-small-image">
          {user !== undefined ? (
            <GetUserImage avatarImageUrl={user.avatarImageUrl} />
          ) : (
            <></>
          )}
        </div>
        <p>{user.userName}</p>
        {changeCommentId !== comment.id ? (
          <p>{comment.text}</p>
        ) : (
          <>
            <input
              className="comment-text-input"
              value={changeCommentText}
              onChange={(event) => setChangeCommentText(event.target.value)}
              type="text"
            />
            <button
              className="comment-button"
              onClick={() => {
                updateComment();
              }}
            >
              Change
            </button>
          </>
        )}
        {userCurrent !== undefined ||
        userCurrent.commentsIds.includes(comment.id) ? (
          <p>
            <span
              onClick={() => {
                if (changeCommentId !== comment.id) {
                  setchangeCommentId(comment.id);
                  setChangeCommentText(comment.text);
                } else {
                  setchangeCommentId(0);
                }
              }}
            >
              Change
            </span>
            |
            {
              <span
                onClick={() => {
                  const deleteComment = async () => {
                    let result = await fetchDeleteComment({
                      commentId: comment.id,
                    });
                    if (result.status === 204) {
                      window.location.reload();
                    }
                  };
                  deleteComment();
                }}
              >
                Delete
              </span>
            }
          </p>
        ) : (
          <></>
        )}
        {replyCommentId === comment.id ? (
          <GameCreateComment
            gameId={props.gameId}
            repliedCommentId={comment.id}
          />
        ) : (
          <p
            onClick={() => {
              setreplyCommentId(comment.id);
            }}
          >
            Reply
          </p>
        )}
        {comment.repliedComment !== undefined ? (
          renderComment(comment.repliedComment, true)
        ) : (
          <></>
        )}
      </div>
    );
  };

  return (
    <>
      <h1>Comments</h1>
      {comments === undefined ? (
        <></>
      ) : comments.length === 0 ? (
        <p>No comments</p>
      ) : (
        getCommentsWithReplied().map((comment) => renderComment(comment))
      )}
      <GameCreateComment gameId={props.gameId} repliedCommentId={0} />
    </>
  );
};

export default GameComments;
