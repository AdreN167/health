import { Button, Heading, Input, Text } from '@chakra-ui/react';
import { useState } from 'react';

const WaitingRoom = ({ joinChat }) => {
    const [userName, setUserName] = useState('');
    const [chatRoom, setChatRoom] = useState('');

    const onSubmit = (e) => {
        e.preventDefault();
        joinChat(userName, chatRoom);
    };

    return (
        <form
            onSubmit={onSubmit}
            className="max-w-sm w-full bg-white p-8 rounded shadow-lg"
        >
            <Heading>Чатик</Heading>
            <div className="mb-4">
                <Text fontSize={'sm'} className="mt-4">
                    Имя пользователя
                </Text>
                <Input
                    onChange={(e) => setUserName(e.target.value)}
                    name="userName"
                    placeholder="Введите ваше имя"
                />
            </div>
            <div className="mb-4">
                <Text fontSize={'sm'} className="mt-4">
                    Название чата
                </Text>
                <Input
                    onChange={(e) => setChatRoom(e.target.value)}
                    name="userName"
                    placeholder="Введите название чата"
                />
            </div>
            <Button type="submit" colorScheme="blue">
                Присоединиться
            </Button>
        </form>
    );
};

export default WaitingRoom;
