import { useState } from "react";

const useInput = (initialValue, validator) => {
  const [value, setValue] = useState(initialValue);
  const [error, setError] = useState(null);

  const handleChange = (e) => {
    const newValue =
      e.target.files !== null ? e.target.files[0] : e.target.value;
    setValue(newValue);

    const { isValid, err } = validator(newValue);

    setError(() => (isValid ? null : err));
  };

  const reset = () => {
    setValue(initialValue), setError(null);
  };

  return {
    value,
    error,
    onchange: handleChange,
    reset,
  };
};

export default useInput;
