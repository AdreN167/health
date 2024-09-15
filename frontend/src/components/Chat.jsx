import { Button, CloseButton, Heading, Input } from '@chakra-ui/react';
import Message from './Message';
import { useEffect, useRef, useState } from 'react';

const Chat = ({ messages, chatRoom, closeChat, sendMessage }) => {
    const [message, setMessage] = useState('');
    const messagesEndRef = useRef();

    useEffect(() => {
        messagesEndRef.current.scrollIntoView();
    }, [messages]);

    const onSendMessage = () => {
        sendMessage(message);
        setMessage('');
    };

    return (
        <div className="w-1/2 bg-white p-8 rounded shadow-lg">
            <div className="flex felx-row justify-between bp-5">
                <Heading size="lg">{chatRoom}</Heading>
                <CloseButton onClick={closeChat} />
            </div>
            <div className="flex flex-col overflow-auto scroll-smooth h-96 gap-3 pb-3">
                {messages.map((messageInfo, index) => {
                    return <Message key={index} messageInfo={messageInfo} />;
                })}
                <span ref={messagesEndRef} />
            </div>
            <div className="flex gap-3">
                <Input
                    type="text"
                    placeholder="Введите сообщение"
                    onChange={(e) => setMessage(e.target.value)}
                    value={message}
                />
                <Button colorScheme="blue" onClick={onSendMessage}>
                    Отправить
                </Button>
            </div>
        </div>
    );
};

export default Chat;
