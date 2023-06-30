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
import CheckIsUserLogin from "../JsFunctions/CheckIsUserLogin";

const GameComments = (props) => {
  const [comments, setComments] = useState("");
  const [users, setUsers] = useState("");
  const [userCurrent, setCurrentUser] = useState({});
  const [changeCommentText, setChangeCommentText] = useState("");
  const [changeCommentId, setchangeCommentId] = useState(0);
  const [showAddCommentButton, setShowAddCommentButton] = useState(false);
  const [removeCommentId, setremoveCommentId] = useState(0);
  const [replyCommentId, setreplyCommentId] = useState(0);
  const [showDeleteSaveButtonId, setShowDeleteSaveButtonId] = useState(0);

  useEffect(() => {
    const getUsers = async () => {
      const result = await fetchUserGetAll();
      let resultJson = await result.json();
      setUsers(resultJson);
    };
    getUsers();

    const getComments = async () => {
      const result = await fetchGetCommentsForGame(props.gameId);
      if (result.ok) {
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
        if (result.ok) {
          let resultjson = await result.json();
          setCurrentUser(resultjson);
        }
      }
    };
    getCurrentUser();
  }, []);

  const DeleteComment = async () => {
    if (removeCommentId !== 0) {
      let result = await fetchDeleteComment({
        commentId: removeCommentId,
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
    }
  };

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

  const updateComment = async () => {
    const result = await fetchUpdateComment({
      commentText: changeCommentText,
      commentId: changeCommentId,
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

  const timeLeft = function(created) {
    var createdDate = new Date(created);

    var now = new Date();

    var timeDifference = now - createdDate;

    var timeDifferencePositive = Math.abs(timeDifference);

    var seconds = Math.floor(timeDifferencePositive / 1000) % 60;
    var minutes = Math.floor(timeDifferencePositive / (1000 * 60)) % 60;
    var hours = Math.floor(timeDifferencePositive / (1000 * 60 * 60)) % 24;
    var days = Math.floor(timeDifferencePositive / (1000 * 60 * 60 * 24));
    return (
      "Time left: " +
      (days !== 0 ? days + " days, " : "") +
      (hours !== 0 ? hours + " hours, " : "") +
      (minutes !== 0 ? minutes + " minutes, " : "") +
      (seconds !== 0 ? seconds + " seconds" : "")
    );
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
        <p>{timeLeft(comment.created)}</p>
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
              Save
            </button>{" "}
            <button
              className="comment-button"
              onClick={() => {
                setchangeCommentId(0);
              }}
            >
              Close
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
              Edit
            </span>
            |
            {showDeleteSaveButtonId !== comment.id ? (
              <span
                onClick={() => {
                  setShowDeleteSaveButtonId(comment.id);
                }}
              >
                Delete
              </span>
            ) : (
              <button
                className="comment-button"
                onClick={() => {
                  setremoveCommentId(comment.id);
                  DeleteComment();
                }}
              >
                Save
              </button>
            )}
          </p>
        ) : (
          <></>
        )}
        {replyCommentId === comment.id ? (
          <GameCreateComment
            gameId={props.gameId}
            repliedCommentId={comment.id}
            closeAddComment={() => {
              setreplyCommentId(0);
            }}
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
      {CheckIsUserLogin() ? (
        showAddCommentButton ? (
          <GameCreateComment
            gameId={props.gameId}
            repliedCommentId={0}
            closeAddComment={() => {
              setShowAddCommentButton(false);
            }}
          />
        ) : (
          <p
            onClick={() => {
              setShowAddCommentButton(true);
            }}
          >
            Comment
          </p>
        )
      ) : (
        <></>
      )}
    </>
  );
};

export default GameComments;
