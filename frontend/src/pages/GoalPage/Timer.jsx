import { useState, useEffect } from "react";
import { Button, Stack, Typography } from "@mui/material";
import StartButton from "../../components/StartButton/StartButton";
import StopButton from "../../components/StopButton/StopButton";

const Timer = ({ onStopClick }) => {
  const [isRunning, setIsRunning] = useState(false);
  const [time, setTime] = useState(0); // время в секундах

  useEffect(() => {
    let timer;
    if (isRunning) {
      timer = setInterval(() => {
        setTime((prevTime) => prevTime + 1); // увеличиваем время каждую секунду
      }, 1000);
    }

    return () => clearInterval(timer); // очищаем таймер при размонтировании или изменении состояния
  }, [isRunning]);

  const toggleTimer = () => {
    setIsRunning((prev) => !prev); // переключаем состояние таймера
    setTime(0);
  };

  const resetTimer = () => {
    setTime(0); // сбрасываем время на ноль
    setIsRunning(false); // останавливаем таймер
    onStopClick();
  };

  const formatTime = (seconds) => {
    const minutes = Math.floor(seconds / 60);
    const secs = seconds % 60;
    return `${String(minutes).padStart(2, "0")}:${String(secs).padStart(
      2,
      "0"
    )}`;
  };

  return (
    <Stack direction="row" alignItems="center">
      {!isRunning ? (
        <StartButton onClick={toggleTimer}>Начать</StartButton>
      ) : (
        <StopButton onClick={resetTimer}>Стоп</StopButton>
      )}
      {isRunning && (
        <Typography sx={{ marginLeft: 2 }}>{formatTime(time)}</Typography>
      )}
    </Stack>
  );
};

export default Timer;
