import React, { useState } from "react";

import { SimpleButton } from "@components/buttons/simple-button";

import styles from "./styles.module.scss";
import downloadSvg from "assets/download.svg";

interface IImageUploader {
  onUpload: (file: File) => void;
  onDelete: () => void;
  [prop: string]: unknown;
}

export const ImageUploader: React.FC<IImageUploader> = props => {
  const [objectURL, setObjectUrl] = useState("");
  const [preview, setPreview] = useState(false);

  const hiddenFileInput = React.createRef<HTMLInputElement>();

  function emulateInputClick(e: Event): void {
    e.preventDefault();
    hiddenFileInput.current?.click();
  }

  function setPreviewSection(file: File): void {
    if (file !== undefined) {
      props.onUpload(file);
      setObjectUrl(URL.createObjectURL(file));
      if (!preview) setPreview(true);
    } else {
      if (preview) setPreview(false);
    }
  }

  function removePreviewSection(e: Event): void {
    e.preventDefault();
    URL.revokeObjectURL(objectURL);
    hiddenFileInput.current!.value = "";
    setObjectUrl("");
    setPreview(false);
    props.onDelete();
  }

  return (
    <div className={styles.uploader}>
      <input
        id="file-upload"
        type="file"
        name="fileUpload"
        accept="image/*"
        ref={hiddenFileInput}
        onChange={e => {
          setPreviewSection(e.target.files![0]);
        }}
      />
      <div className={styles.wrapper}>
        <img
          src={objectURL}
          alt="Preview"
          className={preview ? styles.previewImage : styles.hidden}
        ></img>
        <div className={preview ? styles.hidden : styles.start}>
          <div className={styles.imageWrapper}>
            <img className={styles.downloadSvg} src={downloadSvg}></img>
          </div>
          <SimpleButton
            text={"Select an image"}
            size={"small"}
            onClick={emulateInputClick}
          ></SimpleButton>
        </div>
        <div className={preview ? styles.nameDeleteWrapper : styles.hidden}>
          <SimpleButton
            text={"Delete an image"}
            size={"small"}
            onClick={removePreviewSection}
          ></SimpleButton>
        </div>
      </div>
    </div>
  );
};
