import React, { useEffect, useState } from 'react'
import { v4 as uuidv4 } from 'uuid';
import { Table, Space, Input, Modal, Button } from 'antd';
import { AudioOutlined } from '@ant-design/icons';
import type { ColumnsType, TableProps } from 'antd/es/table';
import { fetchBooks, fetchBorrowing, createBorrowing, updateBookStatus } from '../../api/index';
import { Book } from '../../common/interface';
import { getFullDate, generateBorrowNumber, generateDueDate } from '../../common/util';



const suffix = (
  <AudioOutlined
    style={{
      fontSize: 16,
      color: '#1890ff',
    }}
  />
);

const onChange: TableProps<Book>['onChange'] = (pagination, filters, sorter, extra) => {
  console.log('params', pagination, filters, sorter, extra);
};

/**
 * 
 */
const Home: React.FC = () => {

    const [bookData, setBookData] = useState<Book[]>([]);
    const [filteredBooks, setFilteredBooks] = useState<Book[]>([]);
    const [open, setOpen] = useState(false);
    const [confirmLoading, setConfirmLoading] = useState(false);
    const [modalText, setModalText] = useState('Do you want to borrow this book?');
    
    useEffect(() => {
        fetchBooks().then(data => {
          setBookData(data.data); 
          setFilteredBooks(data.data);
        });
    }, [])

    const onChangeInput = (event: React.ChangeEvent<HTMLInputElement>) => {
        const filteredData = bookData.filter(entry =>
            entry.title.toLowerCase().includes(event.target.value.toLowerCase())
        );
        setFilteredBooks(filteredData);
    }

    const showModal = () => {
        setOpen(true);
      };
    
      
      const generateBorrowing = (targetBook: Book) => {
        let postBorrowingData = {id: uuidv4().toString(), borrowNo: generateBorrowNumber(), userId: "1", bookId: targetBook.id.toString(), dateOfBorrow: new Date().toISOString(), dueDate: generateDueDate(30)};
        // createBorrowing(postBorrowingData);
        updateBookStatus(targetBook);
        setModalText(`Borrowing No. ${postBorrowingData.borrowNo}
        UserId: ${postBorrowingData.userId}
        Book Title: ${targetBook.title}
        BorrowedDate: ${postBorrowingData.dateOfBorrow}
        DueDate: ${postBorrowingData.dueDate}`);
      }
      
      const columns: ColumnsType<Book> = [
        {
          title: 'Title',
          dataIndex: 'title',
          width: '15%',
        },
        {
          title: 'publisher',
          dataIndex: 'publisher',
          width: '15%',
        },
        {
          title: 'Published At',
          dataIndex: 'dateOfPublication',
          render: ((date:string) => getFullDate(date)),
          width: '15%',
        },
        {
          title: 'Status',
          dataIndex: 'isBorrowed',
          render: (text, record) => {
              return {
                  props: {
                      style: {color: !record.isBorrowed ? "green" : "red" }
                  },
                  children: <div>{!text? 'Available':'Unavailable'}</div>
              }
          },
          width: '15%',
        },
        {
          title: '',
          dataIndex: 'isBorrowed',
          render: (_, record) => (
            <Space size="middle" >
              {!record.isBorrowed? 
              <>
                <Button type="primary" onClick={showModal}>
                    Borrow
                </Button>
                <Modal
                    title="Confirm"
                    open={open}
                    onOk={(event) => {
                      handleOk(event, record);
                    }}
                    confirmLoading={confirmLoading}
                    onCancel={handleCancel}
                >
                    <p>{modalText}</p>
                </Modal>
                </>: <></>}
            </Space>
          ),
          width: '20%',
        }
      ];

    const handleOk = (event,targetBook: Book) => {
      generateBorrowing(targetBook);
      setConfirmLoading(true);
      setTimeout(() => {
          setOpen(false);
          setConfirmLoading(false);
      }, 5000);
      window.location.reload();
    };
    
    const handleCancel = () => {
      console.log('Clicked cancel button');
      setOpen(false);
    };

    return (
        <div>
            <Space direction='vertical'>
                <Input
                    placeholder="input search text"
                    size="large"
                    suffix={suffix}
                    onChange={(event) => onChangeInput(event)}
                />
            </Space>
            <Table columns={columns} dataSource={filteredBooks} onChange={onChange}/>
        </div>
        
    )
}


export default Home;