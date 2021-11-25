import React from "react";

import styles from "./styles.module.scss";

interface IInput {
  text: string;
  [prop: string]: unknown;
}

export const Input: React.FC<IInput> = props => {
  return (
    <div className={styles.group}>
      <input {...props} />
      <span className={styles.bar}></span>
      <label>{props.text}</label>
    </div>
  );
};
