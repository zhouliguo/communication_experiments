import socket
import numpy as np
import cv2
import time
import os

#filename = '/tmp/mysock'
#if os.path.exists(filename):
#    os.remove(filename)

socket_s=socket.socket(socket.AF_INET, socket.SOCK_STREAM)             # 创建socket对象
socket_s.bind(('localhost',4323))                                      # 绑定地址
#socket_s.bind(filename)

socket_s.listen(5)                                                     # 建立5个监听

#socket_s.setsockopt(socket.SOL_SOCKET, socket.SO_SNDBUF, 1024*512)
bufsize = socket_s.getsockopt(socket.SOL_SOCKET, socket.SO_SNDBUF)
print(bufsize)

cv2.namedWindow('server', 0)
w = 640
h = 480
required = w*h*3
while True:
    conn, addr= socket_s.accept()                                       # 等待客户端连接
    while True:
        start = time.time()
        data=conn.recv(required)
        while len(data) < required:
            data += conn.recv(required - len(data))
        
        image = np.frombuffer(data, np.uint8).reshape((h, w, 3))
        cv2.imshow('server', image)
        cv2.waitKey(1)
        print((time.time() - start)*1000, 'ms') 
        #dt=data.decode('utf-8')                                 #接收一个1024字节的数据 
        #print('收到：',dt)
        #aa=input('服务器发出：') 
        #if aa=='quit':
        #    conn.close()                                        #关闭来自客户端的连接
        #    s.close()                                           #关闭服务器端连接
        #else:
        #    conn.send(aa.encode('utf-8'))