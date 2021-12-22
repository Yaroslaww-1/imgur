import React, { useContext } from "react";
import { useHistory } from "react-router-dom";

import { Context } from "index";
import { AppRoute } from "@common/enums/app-route.enum";

import styles from "./styles.module.scss";

export const Header: React.FC = () => {
  const { store } = useContext(Context);
  const history = useHistory();

  function toPostCreation() {
    history.push(AppRoute.CREATE_POST);
  }

  function toCommunityCreation() {
    history.push(AppRoute.CREATE_COMMUNITY);
  }

  function toCommunities() {
    history.push(AppRoute.COMMUNITIES_LIST);
  }

  function toUserProfile() {
    history.push(AppRoute.USER_PROFILE);
  }

  function toHome() {
    history.push(AppRoute.HOME);
  }

  function logout() {
    store.logout();
    history.push(AppRoute.LOGIN);
  }

  return (
    <div className={styles.container}>
      <div className={styles.content}>
        <div className={styles.iconContainer}>
          <div className={`${styles.icon} ${styles.iconFill}`} onClick={toHome}>
            <i className="fa fa-home"></i>
          </div>
          <div
            className={`${styles.icon} ${styles.iconFill}`}
            onClick={toPostCreation}
          >
            <i className="fa fa-plus"></i>
          </div>
          <div
            className={`${styles.icon} ${styles.iconFill}`}
            onClick={toCommunityCreation}
          >
            <i className="fa fa-user-plus"></i>
          </div>
          <div
            className={`${styles.icon} ${styles.iconFill}`}
            onClick={toUserProfile}
          >
            <i className="fa fa-user"></i>
          </div>
          <div
            className={`${styles.icon} ${styles.iconFill}`}
            onClick={toCommunities}
          >
            <i className="fa fa-code-fork"></i>
          </div>
          <div className={`${styles.icon} ${styles.iconFill}`} onClick={logout}>
            <i className="fa fa-sign-out"></i>
          </div>
        </div>
      </div>
    </div>
  );
};
