import styles from "./TextButton.module.less";

const TextButton = ({ children, onClick, className }) => {
  return (
    <button className={styles.button + " " + className} onClick={onClick}>
      {children}
    </button>
  );
};

export default TextButton;
