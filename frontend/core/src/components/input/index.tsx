import React from "react";

import styles from "./styles.module.scss";

interface IInput {
  text: string;
  [prop: string]: unknown;
}

export const Input: React.FC<IInput> = props => {
  if (props.text === "") {
    return (
      <div className={styles.group}>
        <input {...props} />
        <span className={styles.bar}></span>
      </div>
    );
  } else {
    return (
      <div className={styles.group}>
        <input {...props} />
        <span className={styles.bar}></span>
        <label>{props.text}</label>
      </div>
    );
  }
};
