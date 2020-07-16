using AuslogicsTest.Commands;
using AuslogicsTest.Enums;
using AuslogicsTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AuslogicsTest
{
    /// <summary>
    /// 
    /// </summary>
    public class MainWindowVM:
            INotifyPropertyChanged,
            IMultiThreadObservableCollectionConsumer<AutoRunItem>,
            IInfo
    {
        #region COMMANDS
        
        private RelayCommand _RefreshCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand RefreshCommand
        {
            get
            {
                return this._RefreshCommand ??
                    ( this._RefreshCommand = new RelayCommand( ( obj ) => { Refresh(); } ) );
            }
        }
        #endregion

        #region INotifyPropertyChanged

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void NotifyPropertyChanged( String propertyName = "" )
        {
            if( PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
            }
        }

        #endregion

        #region IMultiThreadObservableCollectionConsumer
        
        /// <summary>
        /// 
        /// </summary>
        public void Clear( )
        {
            lock( this._AutoRunItemsLock )
            {             
                this._AutoRunItems.Clear();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newItem"></param>
        public void Add( AutoRunItem newItem )
        {
            lock( this._AutoRunItemsLock )
            {
                this._AutoRunItems.Add( newItem );                
            }
        }

        #endregion

        #region interface IInfo
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Message( String message )
        {
            this.InfoText = message;
        }

        #endregion

        private String _InfoText = "000";
        private Object _AutoRunItemsLock;
        private ObservableCollection<AutoRunItem> _AutoRunItems;
                

        /// <summary>
        /// Information
        /// </summary>
        public String InfoText
        {
            get
            {
                return this._InfoText;
            }

            set
            {
                if(String.Compare(this._InfoText, value ) != 0 )
                {
                    this._InfoText = value;
                    NotifyPropertyChanged("InfoText");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<AutoRunItem> AutoRunFiles
        {
            get
            {
                return this._AutoRunItems;
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        private FilesExtractor _FileExtractor = null;

        /// <summary>
        /// 
        /// </summary>
        public void Refresh()
        {
            if( null == this._FileExtractor )
            {
                this._FileExtractor = new FilesExtractor();
            }

            this._FileExtractor.RefreshAutoRunFiles( this, this );
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public MainWindowVM( )
        {
            this._AutoRunItemsLock = new object();
            this._AutoRunItems = new ObservableCollection<AutoRunItem>();
            BindingOperations.EnableCollectionSynchronization( this._AutoRunItems, this._AutoRunItemsLock );
        }
    }
}
